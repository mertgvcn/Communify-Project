import { Validator } from "fluentvalidation-ts";
import { FormDataType } from "../../components/Forms/RegisterForm/types/FormDataType";

export class Form3Validator extends Validator<FormDataType> {
    constructor() {
        super();

        this.ruleFor('birthCountry')
            .notEmpty().withMessage("Required")

        this.ruleFor('birthCity')
            .notEmpty().withMessage("Required")

        this.ruleFor('currentCountry')
            .notEmpty().withMessage("Required")

        this.ruleFor('currentCity')
            .notEmpty().withMessage("Required")

        this.ruleFor('address')
            .notEmpty().withMessage("Required")
    }
}