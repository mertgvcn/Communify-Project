import React from 'react'
import { IconType } from 'react-icons'
//css
import './TextInput.css'
import ValidationError from '../../Alerts/ValidationError/ValidationError'

type TextInputType = {
  width: string | number,
  height: string | number,
  isPassword: boolean,
  errorMessage?: string
  fontSize?: number,
  placeholder?: string,
  name?: string,
  title?: string,
  onChangeFunction?: React.ChangeEventHandler<HTMLInputElement>,
  icon?: IconType
}

const TextInput = (props: TextInputType) => {
  const {width, height, isPassword, errorMessage, fontSize, placeholder, name, title, onChangeFunction} = props

  return (
    <div className='input-template'  style={{ width: width, marginBottom: 10}}>

      <div className='input-wrapper' style={{ height: height }}>
        <input
          type={isPassword ? "password" : "text"}
          className='text-input'
          name={name} placeholder={placeholder} title={title}
          onChange={onChangeFunction}
          style={{
            width: width,
            height: height,
            fontSize: fontSize,
          }} />

        {props.icon &&
          <div className='text-input-icon'>
            <props.icon />
          </div>
        }
      </div>

      <ValidationError errorMessage={errorMessage!}/>
    </div>
  )
}

export default TextInput