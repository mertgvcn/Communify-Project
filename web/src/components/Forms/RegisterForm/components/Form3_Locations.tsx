import React from 'react'
//css
import './styles/Form3_Locations.css'
//types
import { FormDataType } from '../types/FormDataType';
import { FormLocationsType } from '../types/FormLocationsType';
//helpers
import { Form3Validator } from '../../../../validators/RegisterValidators/Form3Validator';
import useDynamicValidation from '../../../../hooks/useDynamicValidation';
//icons
import { FiFlag } from "react-icons/fi";
import { TbBuildingCommunity } from "react-icons/tb";
import { IoLocationOutline } from "react-icons/io5";
//components
import SecondaryButton from '../../../Elements/Buttons/SecondaryButton/SecondaryButton'
import PrimaryButton from '../../../Elements/Buttons/PrimaryButton/PrimaryButton'
import TextInput from '../../../Elements/TextInput/TextInput'

type Form3Type = {
    registerPages: FormLocationsType,
    setRegisterPages: React.Dispatch<React.SetStateAction<FormLocationsType>>,

    formData: FormDataType,
    setFormData: React.Dispatch<React.SetStateAction<FormDataType>>
}

const Form3 = (props: Form3Type) => {
    const formValidator = new Form3Validator()
    const { validationErrors, errorList } = useDynamicValidation(props.formData, formValidator, [props.formData.currentCountry, props.formData.currentCity, props.formData.birthCountry, props.formData.birthCity, props.formData.address])

    const handleChange = (e: any) => {
        const { name, value } = e.target

        props.setFormData({
            ...props.formData, [name]: value
        })
    }

    const handleNext = () => {
        if (Object.keys(errorList).length === 0) {
            props.setRegisterPages({
                Form1: -650,
                Form2: -650,
                Form3: -650,
                Form4: 0,
                Form5: 650,
            })
        }
    }

    return (
        <form id='register-form3' style={{ left: props.registerPages.Form3 }}>

            <div className="form-body">
                <div className='form-title'>
                    <span>Your Location Informations</span>
                </div>

                <div className='inputs'>
                    <div className='row'>

                        <TextInput name="birthCountry" placeholder='Birth Country'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={FiFlag} onChangeFunction={handleChange}
                            errorMessage={validationErrors.birthCountry} />

                        <TextInput name="birthCity" placeholder='Birth City'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={TbBuildingCommunity} onChangeFunction={handleChange}
                            errorMessage={validationErrors.birthCity} />

                    </div>

                    <div className="row">

                        <TextInput name='currentCountry' placeholder='Current Country'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={FiFlag} onChangeFunction={handleChange}
                            errorMessage={validationErrors.currentCountry} />

                        <TextInput name='currentCity' placeholder='Current City'
                            width={200} height={36} fontSize={16} isPassword={false}
                            icon={TbBuildingCommunity} onChangeFunction={handleChange}
                            errorMessage={validationErrors.currentCity} />

                    </div>

                    <TextInput name='address' placeholder='Address'
                        width={440} height={36} fontSize={16} isPassword={false}
                        icon={IoLocationOutline} onChangeFunction={handleChange}
                        errorMessage={validationErrors.address} />

                </div>
            </div>

            <div className='buttons'>
                <SecondaryButton value='Back' width={100} height={40} fontSize={16}
                    onClickFunction={() => {
                        props.setRegisterPages({
                            Form1: -650,
                            Form2: 0,
                            Form3: 650,
                            Form4: 650,
                            Form5: 650,
                        })
                    }} />
                <PrimaryButton value='Next' width={100} height={40} fontSize={16}
                    onClickFunction={handleNext} />
            </div>

        </form>
    )
}
export default Form3
