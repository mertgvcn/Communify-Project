import axios from "axios"
import { getCookie } from "../Cookie"
import { SearchRequest } from "../../models/parameterModels/SearchRequest"
import { SearchResultType } from "../../components/Navbar/components/Searchbar/Searchbar"
import { UserInformationSummaryViewModel } from "../../models/viewModels/UserInformationSummaryViewModel"

const baseUrl = process.env.REACT_APP_BASEURL
const API_KEY = 'bearer ' + getCookie("jwt")

export const Search = async (request: SearchRequest): Promise<SearchResultType> => {
    const response = await axios.post(baseUrl + '/api/Navbar/Search', {
        input: request.input,
        searchType: request.searchType
    })

    return response.data
}

export const getUserInformationSummary = async (): Promise<UserInformationSummaryViewModel> => {
    const response = await axios.get(baseUrl + '/api/Navbar/GetUserInformationSummary',
        {
            headers: {
                'Authorization': API_KEY
            }
        }
    )

    return response.data
}