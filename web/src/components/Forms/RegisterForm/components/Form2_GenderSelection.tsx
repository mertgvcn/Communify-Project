import React from 'react'
//css
import './styles/Form2_GenderSelection.css'
//types
import { FormLocationsType } from '../types/FormLocationsType'
import { Gender } from '../../../../models/entityModels/User'
//components
import SecondaryButton from '../../../Elements/Buttons/SecondaryButton/SecondaryButton'
import PrimaryButton from '../../../Elements/Buttons/PrimaryButton/PrimaryButton'

type Form2Type = {
    registerPages: FormLocationsType,
    setRegisterPages: React.Dispatch<React.SetStateAction<FormLocationsType>>,

    genderState: Gender | null,
    setGenderState: React.Dispatch<React.SetStateAction<Gender | null>>
}

const Form2 = (props: Form2Type) => {

    const genderSelection = (selectedValue: Gender) => {
        props.setGenderState(selectedValue);
    }

    return (
        <form id='register-form2' style={{ left: props.registerPages.Form2 }}>

            <div className='form-body'>
                <div className='form-title'>
                    <span>How do you identify?</span>
                </div>

                <div className='gender-types'>
                    <input
                        id={props.genderState === Gender.Woman ? 'selected' : ''}
                        value="Woman" type='button'
                        onClick={() => { genderSelection(Gender.Woman) }} />
                    <input
                        id={props.genderState === Gender.Man ? 'selected' : ''}
                        value="Man" type='button'
                        onClick={() => { genderSelection(Gender.Man) }} />
                    <input
                        title="only select this if you have psycholigical problems"
                        id={props.genderState === Gender.NonBinary ? 'selected' : ''}
                        value="Non-Binary" type='button'
                        onClick={() => { genderSelection(Gender.NonBinary) }} />
                    <input
                        id={props.genderState === Gender.NotSpecified ? 'selected' : ''}
                        value="I prefer not to say" type='button'
                        onClick={() => { genderSelection(Gender.NotSpecified) }} />
                </div>
            </div>

            <div className='buttons'>
                <SecondaryButton value='Back' width={100} height={40} fontSize={16}
                    onClickFunction={() => {
                        props.setRegisterPages({
                            Form1: 0,
                            Form2: 650,
                            Form3: 650,
                            Form4: 650,
                            Form5: 650,
                        })
                    }} />
                <PrimaryButton value='Next' width={100} height={40} fontSize={16}
                    onClickFunction={() => {
                        if (props.genderState != null) {
                            props.setRegisterPages({
                                Form1: -650,
                                Form2: -650,
                                Form3: 0,
                                Form4: 650,
                                Form5: 650,
                            })
                        }
                        {/* Else için hata eklicez */ }
                    }} />
            </div>
        </form>
    )
}

export default Form2