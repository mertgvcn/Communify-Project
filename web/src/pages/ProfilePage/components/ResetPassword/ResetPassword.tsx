import React, { useEffect, useState } from 'react'
//css
import "./ResetPassword.css"
//helpers

import toast, { Toaster } from 'react-hot-toast'
import TextInput from '../../../../components/Elements/TextInput/TextInput'
import { RiLockPasswordLine } from 'react-icons/ri'
import PrimaryButton from '../../../../components/Elements/Buttons/PrimaryButton/PrimaryButton'
import { IoCloseCircleOutline } from 'react-icons/io5'

type ResetPasswordFormData = {
    oldPassword: string,
    password: string,
    confirmPassword: string
}
type ResetPasswordType = {
    setResetPasswordState: React.Dispatch<React.SetStateAction<boolean>>,
}

const ResetPassword = (props: ResetPasswordType) => {

    //states
    const [formData, setFormData] = useState<ResetPasswordFormData>({
        oldPassword: "",
        password: "",
        confirmPassword: "",
    })
    const [buttonBlocker, setButtonBlocker] = useState(false)

    const handleChange = (e: any) => {
        const { name, value } = e.target

        setFormData({
            ...formData, [name]: value
        })
    }

    const handleSubmit = async () => {
    }

    return (
        <div className="reset-password-background">
            <Toaster toastOptions={{ style: { fontSize: 14 } }} />

            <div className='reset-password-wrapper'>

                <div className='reset-password-form'>
                    <span className='reset-password-title'>Please Set Your New Password</span>

                    <TextInput name='currentPassword' placeholder="Current Password"
                        width={440} height={40} fontSize={16} isPassword={true}
                        icon={RiLockPasswordLine} onChangeFunction={handleChange} />

                    <TextInput name='newPassword' placeholder="New Password"
                        width={440} height={40} fontSize={16} isPassword={true}
                        icon={RiLockPasswordLine} onChangeFunction={handleChange} />

                    <TextInput name='confirmPassword' placeholder="Confirm New Password"
                        width={440} height={40} fontSize={16} isPassword={true}
                        icon={RiLockPasswordLine} onChangeFunction={handleChange} />

                    <div style={{ marginTop: '0.5rem' }}>
                        <PrimaryButton value={'Submit'} width={280} height={40} fontSize={16} disabled={buttonBlocker} onClickFunction={handleSubmit} />
                    </div>
                </div>

            </div>
        </div>
    )
}

export default ResetPassword