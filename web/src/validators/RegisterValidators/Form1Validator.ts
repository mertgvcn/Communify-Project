import { Validator } from "fluentvalidation-ts";
import { FormDataType } from "../../components/Forms/RegisterForm/types/FormDataType";

export class Form1Validator extends Validator<FormDataType> {
    phoneNumberPattern = new RegExp(/^(\+90|0)?\s*(\(\d{3}\)[\s-]*\d{3}[\s-]*\d{2}[\s-]*\d{2}|\(\d{3}\)[\s-]*\d{3}[\s-]*\d{4}|\(\d{3}\)[\s-]*\d{7}|\d{3}[\s-]*\d{3}[\s-]*\d{4}|\d{3}[\s-]*\d{3}[\s-]*\d{2}[\s-]*\d{2})$/);
    birthDate = new RegExp("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.]((19|20)\\d\\d)$")

    constructor() {
        super();

        this.ruleFor('firstName')
            .notEmpty().withMessage("Required")
            .minLength(2).withMessage("First name must be between 2-64 characters")
            .maxLength(64).withMessage("First name must be between 2-64 characters")

        this.ruleFor('lastName')
            .notEmpty().withMessage("Required")
            .minLength(2).withMessage("Last name must be between 2-64 characters")
            .maxLength(64).withMessage("Last name must be between 2-64 characters")

        this.ruleFor("phoneNumber")
            .notEmpty().withMessage("Required")
            .matches(this.phoneNumberPattern).withMessage("Not a valid phone number")

        this.ruleFor("birthDate")
            .notEmpty().withMessage("Required")
            .matches(this.birthDate).withMessage("Not a valid date")

        this.ruleFor("email")
            .notEmpty().withMessage("Required")
            .emailAddress()
    }
}