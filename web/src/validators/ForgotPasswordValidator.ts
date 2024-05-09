import { Validator } from "fluentvalidation-ts";
import { ForgotPasswordDataType } from "../components/Forms/ForgotPasswordForm/ForgotPassword";

export class ForgotPasswordValidator extends Validator<ForgotPasswordDataType> {
    constructor() {
        super();

        this.ruleFor("email")
            .notEmpty().withMessage("Email is required")
            .emailAddress().withMessage("Not a valid email")
    }
}