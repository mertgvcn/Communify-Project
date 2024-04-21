import axios from "axios"
import { PasswordToken } from "../../models/entityModels/Token"

const baseUrl = process.env.REACT_APP_BASEURL

export const GetPasswordTokenByToken = async (token: string): Promise<PasswordToken> => {
    const response = await axios.post(baseUrl + "/api/PasswordToken/GetPasswordTokenByToken", {
        token: token
    })

    return response.data
}