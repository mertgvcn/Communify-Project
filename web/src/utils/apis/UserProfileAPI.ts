import axios from "axios"
import { getCookie } from "../Cookie"
import { UserInformationViewModel } from "../../models/viewModels/UserInformationViewModel"

const baseUrl = process.env.REACT_APP_BASEURL
const API_KEY = 'bearer ' + getCookie("jwt")

export const isProfileOwner = async (username: string): Promise<boolean> => {
    const response = await axios.get(baseUrl + '/api/UserProfile/IsProfileOwner',
        {
            params: {
                username: username
            },
            headers: {
                'Authorization': API_KEY
            }
        }
    )

    return response.data
}

export const getUserInformation = async (): Promise<UserInformationViewModel> => {
    const response = await axios.get(baseUrl + '/api/UserProfile/GetUserInformation',
        {
            headers: {
                'Authorization': API_KEY
            }
        }
    )

    return response.data
}