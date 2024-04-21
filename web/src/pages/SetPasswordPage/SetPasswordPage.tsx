import React, { useEffect, useState } from 'react'
//css
import './SetPasswordPage.css'
//models
import { PasswordToken } from '../../models/entityModels/Token';
//icons
import { RiLockPasswordLine } from "react-icons/ri";
//hooks
import useDynamicValidation from '../../hooks/useDynamicValidation';
//helpers
import { useNavigate, useSearchParams } from 'react-router-dom';
import { setPassword } from '../../utils/apis/AuthenticationAPI';
import { SetPasswordValidator } from '../../validators/RegisterValidators/SetPasswordValidator';
import toast, { Toaster } from 'react-hot-toast';
//components
import TextInput from '../../components/Elements/TextInput/TextInput'
import PrimaryButton from '../../components/Elements/Buttons/PrimaryButton/PrimaryButton';
import ErrorPage from '../ErrorPage/ErrorPage';
import { GetPasswordTokenByToken } from '../../utils/apis/PasswordTokenAPI';
import { SetPasswordRequest } from '../../models/parameterModels/AuthenticationParameterModels';

export type SetPasswordFormData = {
    password: string,
    confirmPassword: string
}

const SetPasswordPage = () => {
    const navigate = useNavigate()
    const [searchParams, setSearchParams] = useSearchParams();

    //states
    const [formData, setFormData] = useState<SetPasswordFormData>({
        password: "",
        confirmPassword: "",
    })
    const [passwordToken, setPasswordToken] = useState<PasswordToken | null>(null)
    const [buttonBlocker, setButtonBlocker] = useState(false)
    
    const formValidator = new SetPasswordValidator()
    const { validationErrors, errorList } = useDynamicValidation(formData, formValidator, [formData.password, formData.confirmPassword])

    useEffect(() => {
        const token = searchParams.get("token")

        if (token != null)
            fetchPasswordTokenByTokenAsync(token)

    }, [searchParams.get("token")])

    //functions
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
                userId: passwordToken!.userId,
                password: formData.password
            }
            await setPassword(setPasswordRequest)

            toast.success("Password set successfully")
            navigate("/", { state: { loginFormState: true } })

            setTimeout(() => {
                setButtonBlocker(false)
                window.location.reload()
            }, 1000)
        }
    }

    const fetchPasswordTokenByTokenAsync = async (token: string) => {
        const response = await GetPasswordTokenByToken(token)
        if(response != null)
            setPasswordToken(response)
    }

    return passwordToken ? (
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