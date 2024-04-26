import React, { useRef, useState } from 'react'
//css
import './Searchbar.css'
//models
import { SearchTypes } from '../../../../models/enums/SearchTypes';
import { SearchRequest } from '../../../../models/parameterModels/SearchRequest';
import { SearchedUserViewModel } from '../../../../models/viewModels/SearchedUserViewModel';
import { SearchedCommunityViewModel } from '../../../../models/viewModels/SearchedCommunityViewModel';
//icons
import { IoMdSearch } from "react-icons/io";
//helpers
import { Search } from '../../../../utils/apis/NavbarAPI';
//components
import SearchResultCard from './components/SearchResultCard';

type SearchResultType = {
    users: SearchedUserViewModel[],
    communities: SearchedCommunityViewModel[]
}

const Searchbar = () => {
    const searchInputRef = useRef("")    
    const [searchResult, setSearchResult] = useState<SearchResultType>({
        users: [],
        communities: []
    })
    const [timer, setTimer] = useState<any>(null)

    const handleChange = (e:React.ChangeEvent<HTMLInputElement>) => {
        searchInputRef.current = e.target.value
        clearTimeout(timer)

        const newTimer = setTimeout(async () => {
            const request: SearchRequest = {
                input: searchInputRef.current,
                searchType: SearchTypes.User
            }

            const users = await Search(request) //ToDo: ilerde sadece user yerine, community ve postta dönmesi gerek
            //temporary
            const results: SearchResultType = {
                users: users,
                communities: []
            }

            setSearchResult(results)
        }, 300)

        setTimer(newTimer)
    }


    return (
        <div className='searchbar-wrapper'>
            <div className='searchbar-icon'>
                <IoMdSearch style={{marginRight:-6}}/>
            </div>
            <input className='searchbar' type="text" placeholder='Search Communify'
                    value={searchInputRef.current} onChange={handleChange}/>


            {(searchResult.users.length > 0 || searchResult.communities.length > 0) &&
                <div className='searchbar-drop-down'>
                    {searchResult.users.map((result, idx) => (
                        <SearchResultCard data={result} key={idx}/>
                    ))}
                </div>
            }
        </div>
    )
}

export default Searchbar