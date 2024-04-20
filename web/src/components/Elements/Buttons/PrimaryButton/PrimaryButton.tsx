import React from 'react'
//type
import { ButtonType } from '../ButtonType'
//css
import './PrimaryButton.css'


const PrimaryButton = (props: ButtonType) => {
  return (
    <input type="button" className="primary-button"
      value={props.value} disabled={props.disabled}
      style={{ width: props.width, height: props.height, fontSize: props.fontSize }}
      onClick={props.onClickFunction} />
  )
}

export default PrimaryButton