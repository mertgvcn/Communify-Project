import React from 'react'
//css
import './ErrorPage.css'
//icons
import { GrStatusUnknown } from "react-icons/gr";


const ErrorPage = () => {
    return (
        <div className='error-page-wrapper'>
            <div className="error-page-container">
                <div style={{fontSize: 200}}>
                    <GrStatusUnknown />
                </div>

                <span>Error: Page Not Found!</span>
            </div>

        </div>
    )
}

export default ErrorPage