import axios from "axios"
//helpers
import { getCookie } from "../Cookie"
//models
import { ForgotPasswordResponse, LoginRequest, LoginResponse, RegisterRequest, RegisterResponse } from "../../models/parameterModels/AuthenticationParameterModels"

const baseUrl = process.env.REACT_APP_BASEURL
const API_KEY = 'bearer ' + getCookie("jwt")

export const isEmailExisting = async (email: string): Promise<boolean> => {
    const response = await axios.post(baseUrl + '/api/Authentication/isEmailExisting', {
        email: email
    })

    return response.data
}

export const login = async (request: LoginRequest): Promise<LoginResponse> => {
    const response = await axios.post(baseUrl + '/api/Authentication/Login', {
        email: request.email,
        password: request.password
    })

    return response.data
}

export const forgotPassword = async (email: string): Promise<ForgotPasswordResponse> => {
    const response = await axios.post(baseUrl + '/api/Authentication/ForgotPassword', {
        email: email
    })

    return response.data
}

export const register = async (request: RegisterRequest): Promise<RegisterResponse> => {
    //Convert string to date Type 
    const dateParts = request.birthDate.split("/");
    const day = parseInt(dateParts[0], 10)
    const month = parseInt(dateParts[1], 10)
    const year = parseInt(dateParts[2], 10)

    const date = new Date(Number(dateParts[2]), Number(dateParts[1])-1, Number(dateParts[0]), 12)

    //Convert interestViewModel to interestIdList
    let interestIdList: number[] = []
    request.interests.forEach(interest => {
        interestIdList.push(interest.id)
    });

    const response = await axios.post(baseUrl + '/api/Authentication/Register', {
        firstName: request.firstName,
        lastName: request.lastName,
        phoneNumber: request.phoneNumber,
        email: request.email,
        birthDate: date,
        gender: request.gender,
        birthCountry: request.birthCountry,
        birthCity: request.birthCity,
        currentCountry: request.currentCountry,
        currentCity: request.currentCity,
        address: request.address,
        interestIdList: interestIdList,
    })

    return response.data
}

export const setPassword = async (password: string): Promise<boolean> => {
    const response = await axios.post(baseUrl + '/api/Authentication/SetPassword', {
        password: password
    }, {
        headers: {
            'Authorization': API_KEY
        }
    })

    return response.data
}
