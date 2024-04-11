import { Validator } from "fluentvalidation-ts";
import { SetPasswordFormData } from "../../pages/SetPasswordPage/SetPasswordPage";

export class SetPasswordValidator extends Validator<SetPasswordFormData> {
    passwordPattern = new RegExp(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(\S).{8,20}$/)

    constructor() {
        super();

        this.ruleFor("password")
            .notEmpty().withMessage("Password is required")
            .matches(this.passwordPattern).withMessage("Password must contain at least 8 characters, 1 capital letter and 1 number")

        this.ruleFor("confirmPassword")
            .must((value, context) => value === context.password).withMessage("Confirm your password")
    }
}