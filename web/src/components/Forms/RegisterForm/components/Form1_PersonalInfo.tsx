import React, { useState } from 'react'
//css
import './Form1_PersonalInfo.css'
//types
import { FormLocationsType } from '../types/FormLocationsType'
import { FormDataType } from '../types/FormDataType'
//helpers
import { isEmailAvailable } from '../../../../utils/apis/AuthenticationAPI'
import { Form1Validator } from '../../../../validators/RegisterValidators/Form1Validator'
//icons
import { GrPhone } from 'react-icons/gr'
import { MdOutlineMail } from 'react-icons/md'
import { PiUserBold } from 'react-icons/pi'
import { TbCalendar } from 'react-icons/tb'
//components
import PrimaryButton from '../../../Elements/Buttons/PrimaryButton/PrimaryButton'
import TextInput from '../../../Elements/TextInput/TextInput'

type Form1Type = {
    registerPages: FormLocationsType,
    setRegisterPages: React.Dispatch<React.SetStateAction<FormLocationsType>>,

    formData: FormDataType,
    setFormData: React.Dispatch<React.SetStateAction<FormDataType>>
}

const Form1 = (props: Form1Type) => {
    const formValidator = new Form1Validator()
    const [validationErrors, setValidationErrors] = useState<any>({})

    const handleChange = (e: any) => {
        const { name, value } = e.target

        props.setFormData({
            ...props.formData, [name]: value
        })
    }

    const handleNext = async () => {
        const errors = formValidator.validate(props.formData)
        setValidationErrors(errors)

        if (Object.keys(errors).length === 0) {
            if (await isEmailAvailable(props.formData.email)) {
                props.setRegisterPages({
                    Form1: -650,
                    Form2: 0,
                    Form3: 650,
                    Form4: 650,
                    Form5: 650,
                })
            }
            else {
                setValidationErrors({...errors, email: "This email is not available"})
            }
        }
    }

    return (
        <form id='register-form1' style={{ left: props.registerPages.Form1 }}>

            <div className='form-body'>
                <div className='form-title'>
                    <span>Create Account</span>
                </div>


                <div className='inputs'>

                    <div className='row' style={{ display: 'flex', justifyContent: 'space-between' }}>

                        <TextInput name='firstName' placeholder='First Name'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={PiUserBold} onChangeFunction={handleChange}
                            errorMessage={validationErrors.firstName} />

                        <TextInput name='lastName' placeholder='Last Name'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={PiUserBold} onChangeFunction={handleChange}
                            errorMessage={validationErrors.lastName} />

                    </div>

                    <div className='row' style={{ display: 'flex', justifyContent: 'space-between' }}>

                        <TextInput name='phoneNumber' placeholder='Phone Number'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={GrPhone} onChangeFunction={handleChange}
                            errorMessage={validationErrors.phoneNumber} />

                        <TextInput name='birthDate' placeholder='Birth Date' title='dd/mm/yyyy'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={TbCalendar} onChangeFunction={handleChange}
                            errorMessage={validationErrors.birthDate} />

                    </div>

                    <TextInput name='email' placeholder='Email'
                        width={440} height={36} fontSize={16} isPassword={false}
                        icon={MdOutlineMail} onChangeFunction={handleChange}
                        errorMessage={validationErrors.email} />

                </div>
            </div>


            <div className="buttons">
                <div style={{ float: 'right' }}>
                    <PrimaryButton value={'Next'} width={100} height={40} fontSize={16}
                        onClickFunction={handleNext} />
                </div>
            </div>
        </form>
    )
}

export default Form1