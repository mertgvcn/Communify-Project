import React, { useRef, useState } from 'react'
//css
import './Searchbar.css'
//icons
import { IoMdSearch } from "react-icons/io";


const Searchbar = () => {
    const searchInputRef = useRef("")    
    const [timer, setTimer] = useState<any>(null)

    const handleChange = (e:React.ChangeEvent<HTMLInputElement>) => {
        searchInputRef.current = e.target.value
        clearTimeout(timer)

        const newTimer = setTimeout(() => {

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

        </div>
    )
}

export default Searchbar