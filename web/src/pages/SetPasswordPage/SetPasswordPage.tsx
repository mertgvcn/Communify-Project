import React, { useState } from 'react'
//css
import './SetPasswordPage.css'
//icons
import { RiLockPasswordLine } from "react-icons/ri";
//hooks
import useDynamicValidation from '../../hooks/useDynamicValidation';
//helpers
import { useNavigate } from 'react-router-dom';
import { deleteCookie } from '../../utils/Cookie';
import { setPassword } from '../../utils/apis/AuthenticationAPI';
import { SetPasswordValidator } from '../../validators/RegisterValidators/SetPasswordValidator';
import toast, { Toaster } from 'react-hot-toast';
//components
import TextInput from '../../components/Elements/TextInput/TextInput'
import PrimaryButton from '../../components/Elements/Buttons/PrimaryButton/PrimaryButton';

export type SetPasswordFormData = {
    password: string,
    confirmPassword: string
}

const SetPasswordPage = () => {
    const navigate = useNavigate()
    const formValidator = new SetPasswordValidator()

    //states
    const [formData, setFormData] = useState<SetPasswordFormData>({
        password: "",
        confirmPassword: "",
    })
    const { validationErrors, errorList } = useDynamicValidation(formData, formValidator, [formData.password, formData.confirmPassword])
    const [buttonBlocker, setButtonBlocker] = useState(false)

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

            await setPassword(formData.password)

            deleteCookie("jwt")
            toast.success("Password set successfully")
            navigate("/", { state: { loginFormState: true } })
            
            setTimeout(() => {
                setButtonBlocker(false)
                window.location.reload()
            }, 1000)
        }
    }

    return (
        <div className="set-password-wrapper">
            <Toaster toastOptions={{style: {fontSize: 14}}}/>

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
    )
}

export default SetPasswordPage