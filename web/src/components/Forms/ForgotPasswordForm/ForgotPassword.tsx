import React from 'react'
//css
import './ForgotPassword.css'
//types
import { FormStateType } from '../RegisterForm/types/FormStateType';
//icons
import { IoMdArrowBack } from "react-icons/io";
import { IoCloseCircleOutline } from "react-icons/io5";
import { MdOutlineMail } from "react-icons/md";
//components
import TextInput from '../../Elements/TextInput/TextInput';
import PrimaryButton from '../../Elements/Buttons/PrimaryButton/PrimaryButton';

type ForgotPasswordType = {
  setForgotPasswordState: React.Dispatch<React.SetStateAction<boolean>>,
  setFormState: React.Dispatch<React.SetStateAction<FormStateType>>,
}

const ForgotPassword = (props: ForgotPasswordType) => {
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

            <TextInput width={460} height={40} fontSize={16} isPassword={false} placeholder='Please enter your email' icon={MdOutlineMail} />
          </div>

        </div>

        <div className='confirm-button'>
          <PrimaryButton width={460} height={36} value='Send an email' onClickFunction={() => { }} />
        </div>
        
      </div>
    </div>
  )
}

export default ForgotPassword