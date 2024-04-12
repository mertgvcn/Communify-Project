import React from 'react'
//types
import { ButtonType } from '../ButtonType'
//css
import './TextButton.css'

const TextButton = (props: ButtonType) => {
    return (
        <input type='button' className="text-button"
            value={props.value}
            style={{ width: props.width, height: props.height, fontSize: props.fontSize }}
            onClick={props.onClickFunction}
        />
    )
}

export default TextButton