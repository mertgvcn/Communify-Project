import React from 'react'
//css
import './Searchbar.css'
//icons
import { IoMdSearch } from "react-icons/io";


const Searchbar = () => {
    return (
        <div className='searchbar-wrapper'>
            <div className='searchbar-icon'>
                <IoMdSearch style={{marginRight:-6}}/>
            </div>
            <input className='searchbar' type="text" placeholder='Search Communify'/>

        </div>
    )
}

export default Searchbar