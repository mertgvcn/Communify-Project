import React from 'react'
//css
import './SearchResultCard.css'
//icons
import { FaUserCircle } from "react-icons/fa";
//models
import { SearchedUserViewModel } from '../../../../../models/viewModels/SearchedUserViewModel'

type SearchResultCardType = {
    data: SearchedUserViewModel
}

const SearchResultCard = (props: SearchResultCardType) => {
    return (
        <div className='search-result-card-wrapper'>
            <div className='icon'>
                <FaUserCircle />
            </div>

            <div className='info'>
                <span className='full-name'>{props.data.fullName}</span>
                <span className='username'>#{props.data.username}</span>
            </div>        
        </div>
    )
}

export default SearchResultCard