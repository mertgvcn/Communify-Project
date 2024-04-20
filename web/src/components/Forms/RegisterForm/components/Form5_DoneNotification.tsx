import React from 'react'
//css
import './styles/Form5_DoneNotification.css'
//types
import { FormLocationsType } from '../types/FormLocationsType';
import { FormStateType } from '../types/FormStateType';
//components
import PrimaryButton from '../../../Elements/Buttons/PrimaryButton/PrimaryButton';

type Form5Type = {
    registerPages: FormLocationsType,
    setRegisterPages: React.Dispatch<React.SetStateAction<FormLocationsType>>,

    setFormState: React.Dispatch<React.SetStateAction<FormStateType>>,
}

const Form5 = (props: Form5Type) => {
    
    const handleDone = () => {
        props.setRegisterPages({
            Form1: 0,
            Form2: 650,
            Form3: 650,
            Form4: 650,
            Form5: 650
        })
        props.setFormState({
            loginFormState: false,
            registerFormState: false
        })
    }

    return (
        <form id='register-form5' style={{ left: props.registerPages.Form5 }}>

            <div className='form-title'>
                <span>There is one last step!</span>
            </div>

            <span style={{ width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center', textAlign: 'center', fontSize: 16 }}>
                We have sent a link to your email to create your password. Before logging in to Communify, please check your email and set your password.
            </span>

            <div className='button' style={{ width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center', marginTop: 40 }}>
                <PrimaryButton value='Done' width={100} height={40} fontSize={16} onClickFunction={handleDone} />
            </div>

        </form>
    )
}

export default Form5