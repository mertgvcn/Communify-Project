import React, { useState } from 'react';
//css
import './LoginForm.css'
//types
import { FormStateType } from '../RegisterForm/types/FormStateType';
//hooks
import useDynamicValidation from '../../../hooks/useDynamicValidation';
//models
import { InterestViewModel } from '../../../models/viewModels/InterestViewModel';
import { LoginRequest, LoginResponse } from '../../../models/parameterModels/AuthenticationParameterModels';
//helpers
import { GetInterests } from '../../../utils/apis/InterestAPI';
import { login } from '../../../utils/apis/AuthenticationAPI';
import { setCookie } from '../../../utils/Cookie';
import { LoginValidator } from '../../../validators/LoginValidator/LoginValidator';
import toast, { Toaster } from 'react-hot-toast';
//icons
import { RiLockPasswordLine } from "react-icons/ri";
import { IoCloseCircleOutline } from "react-icons/io5";
import { PiUserBold } from 'react-icons/pi'
//components
import PrimaryButton from '../../Elements/Buttons/PrimaryButton/PrimaryButton';
import SecondaryButton from '../../Elements/Buttons/SecondaryButton/SecondaryButton';
import TextButton from '../../Elements/Buttons/TextButton/TextButton';
import TextInput from '../../Elements/TextInput/TextInput';

export type FormDataType = {
    credential: string,
    password: string
}

type LoginFormType = {
    setFormState: React.Dispatch<React.SetStateAction<FormStateType>>,
    setForgotPasswordState: React.Dispatch<React.SetStateAction<boolean>>,
    setInterestList: React.Dispatch<React.SetStateAction<InterestViewModel[]>>
}

const LoginForm = (props: LoginFormType) => {
    const formValidator = new LoginValidator()

    //states
    const [formData, setFormData] = useState<FormDataType>({
        credential: "",
        password: ""
    });
    const { validationErrors, errorList } = useDynamicValidation(formData, formValidator, [formData.credential, formData.password])
    const [buttonBlocker, setButtonBlocker] = useState(false)

    //functions
    const handleChange = (e: any) => {
        const { name, value } = e.target

        setFormData({
            ...formData, [name]: value
        })
    }

    const handleLogin = async () => {
        if (Object.keys(errorList).length === 0) {
            setButtonBlocker(true)

            const loginRequest: LoginRequest = {
                credential: formData.credential,
                password: formData.password
            }

            const response: LoginResponse = await login(loginRequest)

            if (response.authenticateResult) {
                setCookie("jwt", response.authToken, response.accessTokenExpireDate)
                toast.success("Login successful.")

                setTimeout(() => {
                    window.location.href = "/"
                }, 1000)
            }
            else {
                toast.error("Your credentials are wrong.", {duration: 2000})
            }
        }

        setTimeout(() => {
            setButtonBlocker(false)
        }, 2000)
    }

    const handleForgotPassword = () => {
        toast.dismiss()

        props.setFormState({
            loginFormState: false,
            registerFormState: false
        })

        props.setForgotPasswordState(true)
    }

    const handleRegisterForm = async () => {
        toast.dismiss()

        await fetchInterestList()

        props.setFormState({
            loginFormState: false,
            registerFormState: true
        })
    }

    const handleClose = () => {
        toast.dismiss()

        props.setFormState({ loginFormState: false, registerFormState: false })
    }

    const fetchInterestList = async () => {
        const response: any = await GetInterests()

        props.setInterestList(response)
    }

    return (
        <div className='login-form-background'>
            <Toaster toastOptions={{ style: { fontSize: 14 } }} />

            <div className='login-form-wrapper'>

                <div className="login-form">
                    <div className='login-form-logo'>
                        <img src={require(`../../../assets/logos/small_logo.png`)} alt="img not found" />
                    </div>

                    <span id='login-message'>Log in to Communify</span>

                    <TextInput name="credential" placeholder='Email or username'
                        width={280} height={40} fontSize={16} isPassword={false}
                        icon={PiUserBold} onChangeFunction={handleChange}
                        errorMessage={validationErrors.credential} />

                    <TextInput name="password" placeholder='Password'
                        width={280} height={40} fontSize={16} isPassword={true}
                        icon={RiLockPasswordLine} onChangeFunction={handleChange}
                        errorMessage={validationErrors.password} />

                    <div style={{ marginTop: '0.5rem' }}>
                        <TextButton value={'Forgot password?'} width={280} height={40} fontSize={16} onClickFunction={handleForgotPassword} />
                        <PrimaryButton value={'Log In'} width={280} height={40} fontSize={16} disabled={buttonBlocker} onClickFunction={handleLogin} />
                    </div>
                </div>

                <div className="go-register" style={
                    {
                        backgroundImage: `url(${require("../../../assets/images/login-form-pic.png")}`,
                    }
                }>
                    <div className='close-button-wrapper'>
                        <IoCloseCircleOutline style={{ float: 'right', cursor: 'pointer' }} onClick={handleClose} />
                    </div>

                    <div className="go-register-body">
                        <p className='title'  >Hello Communifier!</p>
                        <span className='body'>Join our community and unlock a world of sharing. Sign up now to connect and communify!</span>

                        <div className='signup-button-wrapper'>
                            <SecondaryButton value={'Sign Up'} width={120} height={40} fontSize={16} onClickFunction={handleRegisterForm} />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    )
};

export default LoginForm;
