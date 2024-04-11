import e from 'express'
import React, { useEffect, useRef, useState } from 'react'

/*
    object    -> validation edilecek obje; form objesi vs olabilir.
    validator -> obje üzerinde hangi validation rulesların çalışacağını belirler.
    deps      -> useEffectin çalışma koşulu.
*/

const useDynamicValidation = (object: any, validator: any, deps: Array<any>) => {
    const [validationErrors, setValidationErrors] = useState<any>({})
    const errors = useRef({})

    useEffect(() => {
        errors.current = validator.validate(object)
        setValidationErrors(errors.current)
    }, deps)

    const errorList = errors.current

    return { validationErrors, errorList}
}

export default useDynamicValidation