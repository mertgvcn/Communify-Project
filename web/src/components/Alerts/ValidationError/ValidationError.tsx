import React, { useEffect, useState } from 'react'
//css
import './ValidationError.css'

type ValidationErrorType = {
  errorMessage: string,
}


const ValidationError = (props: ValidationErrorType) => {
  const { errorMessage } = props
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    setIsVisible(errorMessage?.length > 0);
  }, [errorMessage]);

  return (
    <div className={`validation-error-wrapper ${isVisible ? 'show' : ''}`} >
      <span className='error-message'>{errorMessage}</span>
    </div>
  )
}

export default ValidationError