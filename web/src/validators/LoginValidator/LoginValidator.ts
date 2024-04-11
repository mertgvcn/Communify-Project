import { Validator } from "fluentvalidation-ts";
import { FormDataType } from "../../components/Forms/LoginForm/LoginForm";

export class LoginValidator extends Validator<FormDataType> {
    constructor() {
        super();

        this.ruleFor("email")
        .notEmpty().withMessage("Required")
        .emailAddress().withMessage("Not a valid email")

        this.ruleFor("password")
        .notEmpty().withMessage("Required")
    }
}