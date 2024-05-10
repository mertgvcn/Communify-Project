import { Validator } from "fluentvalidation-ts";
import { UserInformationViewModel } from "../models/viewModels/UserInformationViewModel";


export class EditProfileValidator extends Validator<UserInformationViewModel> {
    phoneNumberPattern = new RegExp(/^(\+90|0)?\s*(\(\d{3}\)[\s-]*\d{3}[\s-]*\d{2}[\s-]*\d{2}|\(\d{3}\)[\s-]*\d{3}[\s-]*\d{4}|\(\d{3}\)[\s-]*\d{7}|\d{3}[\s-]*\d{3}[\s-]*\d{4}|\d{3}[\s-]*\d{3}[\s-]*\d{2}[\s-]*\d{2})$/);
    currentPlace = new RegExp(/^[A-Za-z]{2,}\/[A-Za-z]{2,}$/);

    constructor() {
        super();

        this.ruleFor('firstName')
            .notEmpty().withMessage("First name is required")
            .minLength(2).withMessage("First name must be between 2-64 characters")
            .maxLength(64).withMessage("First name must be between 2-64 characters")

        this.ruleFor('lastName')
            .notEmpty().withMessage("Last name is required")
            .minLength(2).withMessage("Last name must be between 2-64 characters")
            .maxLength(64).withMessage("Last name must be between 2-64 characters")

        this.ruleFor("username")
            .notEmpty().withMessage("Username is required")
            .maxLength(32).withMessage("Last name must be between 32 characters")

        this.ruleFor("email")
            .notEmpty().withMessage("Email is required")
            .emailAddress()

        this.ruleFor("phoneNumber")
            .notEmpty().withMessage("Phone number is required")
            .matches(this.phoneNumberPattern).withMessage("Not a valid phone number")

        this.ruleFor("currentCountry")
            .notEmpty().withMessage("Current country is required")
            .maxLength(256).withMessage("Country must contain a maximum of 64 characters")

        this.ruleFor("currentCity")
            .notEmpty().withMessage("Current city is required")
            .maxLength(256).withMessage("City must contain a maximum of 64 characters")

        this.ruleFor("address")
            .notEmpty().withMessage("Address is required")
            .maxLength(256).withMessage("Address must contain a maximum of 256 characters")

    }
}