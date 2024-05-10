import axios from "axios"
import { getCookie } from "../Cookie"
import { ProfilePageDataModel } from "../../models/pageViewModels/ProfilePageDataModel"
import { UserInformationViewModel } from "../../models/viewModels/UserInformationViewModel"

const baseUrl = process.env.REACT_APP_BASEURL
const API_KEY = 'bearer ' + getCookie("jwt")

export const getProfilePageData = async (username: string): Promise<ProfilePageDataModel> => {
    const response = await axios.get(baseUrl + '/api/ProfilePage/GetProfilePageData',
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
    const response = await axios.get(baseUrl + '/api/ProfilePage/GetUserInformation',
        {
            headers: {
                'Authorization': API_KEY
            }
        }
    )

    return response.data
}

export const toggleFollowUser = async (username: string, isFollower: boolean): Promise<boolean> => {
    const response = await axios.post(baseUrl + '/api/ProfilePage/ToggleFollowUser', {
        username: username,
        isFollower: isFollower
    },
        {
            headers: {
                'Authorization': API_KEY
            }
        })

    return response.data
}