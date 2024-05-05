import React, { useEffect, useState } from 'react'
//css
import './SetPasswordPage.css'
//models
import { SetPasswordRequest } from '../../models/parameterModels/AuthenticationParameterModels';
import { TokenViewModel } from '../../models/viewModels/TokenViewModel';
//icons
import { RiLockPasswordLine } from "react-icons/ri";
//hooks
import useDynamicValidation from '../../hooks/useDynamicValidation';
//helpers
import { useNavigate, useSearchParams } from 'react-router-dom';
import { setPassword } from '../../utils/apis/AuthenticationAPI';
import { SetPasswordValidator } from '../../validators/SetPasswordValidator';
import { PasswordTokenExists } from '../../utils/apis/PasswordTokenAPI';
import toast, { Toaster } from 'react-hot-toast';
//components
import TextInput from '../../components/Elements/TextInput/TextInput'
import PrimaryButton from '../../components/Elements/Buttons/PrimaryButton/PrimaryButton';
import ErrorPage from '../ErrorPage/ErrorPage';
import { jwtDecode } from 'jwt-decode';

export type SetPasswordFormData = {
    password: string,
    confirmPassword: string
}

const SetPasswordPage = () => {
    const navigate = useNavigate()
    const [searchParams, setSearchParams] = useSearchParams();
    const token = searchParams.get("token")

    //states
    const [formData, setFormData] = useState<SetPasswordFormData>({
        password: "",
        confirmPassword: "",
    })
    const [validPasswordToken, setValidPasswordToken] = useState(false)
    const [buttonBlocker, setButtonBlocker] = useState(false)

    const formValidator = new SetPasswordValidator()
    const { validationErrors, errorList } = useDynamicValidation(formData, formValidator, [formData.password, formData.confirmPassword])

    //functions
    useEffect(() => {
        if (token != null)
            CheckPasswordTokenExists(token!)
    }, [])

    const CheckPasswordTokenExists = async (token: string) => {
        const response = await PasswordTokenExists(token)
        setValidPasswordToken(response)
    }

    const handleChange = (e: any) => {
        const { name, value } = e.target

        setFormData({
            ...formData, [name]: value
        })
    }

    const handleSubmit = async () => {
        if (Object.keys(errorList).length === 0) {
            setButtonBlocker(true)

            const setPasswordRequest: SetPasswordRequest = {
                token: token!,
                password: formData.password
            }
            const response = setPassword(setPasswordRequest)

            await toast.promise(
                response,
                {
                    loading: 'Please wait...',
                    success: <b>Password set successfully.</b>,
                    error: null
                }
            )

            setTimeout(() => {
                setButtonBlocker(false)
                navigate("/", { state: { loginFormState: true } })
            }, 1000)
        }
    }

    return validPasswordToken ? (
        <div className="set-password-wrapper">
            <Toaster toastOptions={{ style: { fontSize: 14 } }} />

            <div className='set-password-container'>
                <span className='set-password-title'>Please Set Your Password</span>

                <div>
                    <TextInput name='password' placeholder="Password"
                        width={440} height={40} fontSize={16} isPassword={true}
                        icon={RiLockPasswordLine} onChangeFunction={handleChange}
                        errorMessage={validationErrors.password} />

                    <TextInput name='confirmPassword' placeholder="Confirm Password"
                        width={440} height={40} fontSize={16} isPassword={true}
                        icon={RiLockPasswordLine} onChangeFunction={handleChange}
                        errorMessage={validationErrors.confirmPassword} />
                </div>

                <PrimaryButton value={'Submit'} width={440} height={40} fontSize={16} disabled={buttonBlocker} onClickFunction={handleSubmit} />
            </div>
        </div>
    ) : <ErrorPage />
}

export default SetPasswordPage