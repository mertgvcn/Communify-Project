import React, { useState } from 'react'
//css
import './ForgotPassword.css'
//types
import { FormStateType } from '../RegisterForm/types/FormStateType';
//icons
import { IoMdArrowBack } from "react-icons/io";
import { IoCloseCircleOutline } from "react-icons/io5";
import { MdOutlineMail } from "react-icons/md";
//helpers
import { setCookie } from '../../../utils/Cookie';
import { ForgotPasswordValidator } from '../../../validators/RegisterValidators/ForgotPasswordValidator';
import { forgotPassword, isEmailExists } from '../../../utils/apis/AuthenticationAPI';
import useDynamicValidation from '../../../hooks/useDynamicValidation';
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
      if (await isEmailExists(formData.email)) {
        const response = await forgotPassword(formData.email)
        
        if (response.isSuccess)
          setCookie("jwt", response.token, response.tokenExpireDate)

        props.setForgotPasswordState(false)
      }
      else {
        //toast notification eklenecek
      }
    }
  }

  return (
    <div className='forgot-password-background'>
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
            <span className='information-message'>Please enter your email address below. We will send you a link to reset your password. Once you receive the email, click on the link to create your new password.</span>

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