import { Validator } from "fluentvalidation-ts";
import { FormDataType } from "../components/Forms/RegisterForm/types/FormDataType";

export class Form3Validator extends Validator<FormDataType> {
    constructor() {
        super();

        this.ruleFor('birthCountry')
            .notEmpty().withMessage("Birth country is required")

        this.ruleFor('birthCity')
            .notEmpty().withMessage("Birth city is required")

        this.ruleFor('currentCountry')
            .notEmpty().withMessage("Current country is required")

        this.ruleFor('currentCity')
            .notEmpty().withMessage("Current city is required")

        this.ruleFor('address')
            .notEmpty().withMessage("Address is required")
    }
}