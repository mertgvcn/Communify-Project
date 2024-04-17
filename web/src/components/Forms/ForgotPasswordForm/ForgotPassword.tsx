import React, { useState } from 'react'
//css
import './ForgotPassword.css'
//types
import { FormStateType } from '../RegisterForm/types/FormStateType';
import { ForgotPasswordResponse } from '../../../models/parameterModels/AuthenticationParameterModels';
//icons
import { IoMdArrowBack } from "react-icons/io";
import { IoCloseCircleOutline } from "react-icons/io5";
import { MdOutlineMail } from "react-icons/md";
//hooks
import useDynamicValidation from '../../../hooks/useDynamicValidation';
//helpers
import { setCookie } from '../../../utils/Cookie';
import { ForgotPasswordValidator } from '../../../validators/RegisterValidators/ForgotPasswordValidator';
import { forgotPassword, EmailExists } from '../../../utils/apis/AuthenticationAPI';
import toast, { Toaster } from 'react-hot-toast';
//components
import TextInput from '../../Elements/TextInput/TextInput';
import PrimaryButton from '../../Elements/Buttons/PrimaryButton/PrimaryButton';

export type ForgotPasswordDataType = {
  email: string
}

type ForgotPasswordType = {
  setForgotPasswordState: React.Dispatch<React.SetStateAction<boolean>>,
  setFormState: React.Dispatch<React.SetStateAction<FormStateType>>,
}

const ForgotPassword = (props: ForgotPasswordType) => {
  const formValidator = new ForgotPasswordValidator()

  const [formData, setFormData] = useState<ForgotPasswordDataType>({
    email: ""
  })
  const { validationErrors, errorList } = useDynamicValidation(formData, formValidator, [formData.email])

  //functions
  const handleChange = (e: any) => {
    const { name, value } = e.target

    setFormData({
      ...formData, [name]: value
    })
  }

  const handleSendEmail = async () => {
    if (Object.keys(errorList).length === 0) {
      if (await EmailExists(formData.email)) {
        const response = forgotPassword(formData.email)

        toast.promise(
          response,
          {
            loading: 'Email sending...',
            success: <b>Email successfully sent.</b>,
            error: <b>Email could not sent!</b>,
          }
        )

        const data: ForgotPasswordResponse = (await response)

        if (data.isSuccess) {
          setCookie("jwt", data.token, data.tokenExpireDate)

          setTimeout(() => {
            props.setForgotPasswordState(false)
          }, 1000)
        }
      }
      else {
        toast.error("This email does not exist!")
      }
    }
    else {
      toast.error("Please enter a valid email.")
    }
  }

  return (
    <div className='forgot-password-background'>
      <Toaster toastOptions={{ style: { fontSize: 14 } }} />

      <div className="forgot-password-wrapper">

        <div className="row">

          <div className="navigation-buttons">
            <div className='back-button'>
              <IoMdArrowBack style={{ cursor: 'pointer' }}
                onClick={() => {
                  props.setForgotPasswordState(false)
                  props.setFormState({ loginFormState: true, registerFormState: false })
                }} />
            </div>

            <div className='close-button'>
              <IoCloseCircleOutline style={{ cursor: 'pointer' }}
                onClick={() => props.setForgotPasswordState(false)} />
            </div>
          </div>

          <div className="forgot-password-body">
            <span className='title'>Reset your password</span>
            <span className='information-message'>Please enter your email address below. If we find the account you will recieve an email to reset your password. Once you receive the email, click on the link to change your password.</span>

            <TextInput width={460} height={40} fontSize={16} isPassword={false}
              name='email' placeholder='Please enter your email'
              onChangeFunction={handleChange} icon={MdOutlineMail}
              errorMessage={validationErrors.email} />
          </div>

        </div>

        <div className='confirm-button'>
          <PrimaryButton width={460} height={36} value='Send an email' onClickFunction={handleSendEmail} />
        </div>

      </div>
    </div>
  )
}

export default ForgotPassword