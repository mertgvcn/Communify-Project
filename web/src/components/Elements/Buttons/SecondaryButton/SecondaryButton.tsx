import React from 'react'
//type
import { ButtonType } from '../ButtonType'
//css
import './SecondaryButton.css'

const SecondaryButton = (props: ButtonType) => {
    return (
        <input type="button" className="secondary-button"
            value={props.value}
            style={{ width: props.width, height: props.height, fontSize: props.fontSize }}
            onClick={props.onClickFunction} />
    )
}

export default SecondaryButton