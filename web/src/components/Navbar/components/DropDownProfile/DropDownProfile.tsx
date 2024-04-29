import React from 'react'
//css
import './DropDownProfile.css'
//icons
import { FaUser } from "react-icons/fa";
import { IoMdSettings } from "react-icons/io";
import { FaSignOutAlt } from "react-icons/fa";
//helpers
import { deleteCookie } from '../../../../utils/Cookie';
import { useNavigate } from 'react-router';

type dropDownProfileType = {
    setDropDownProfileState : React.Dispatch<React.SetStateAction<boolean>>
}

const DropDownProfile = (props : dropDownProfileType) => {
    const navigate = useNavigate()

    const handleProfile = async () => {
        navigate("/profile")
        props.setDropDownProfileState(false)
    }

    const handleLogout = () => {
        navigate("/", { state: { loginFormState: true } })
        deleteCookie("jwt")

        setTimeout(() => {
            window.location.reload()
        }, 300)
    }

    return (
        <div className='drop-down-profile'>
            <ul className='list-wrapper'>

                <li style={{ borderTopLeftRadius: 10, borderTopRightRadius: 10 }} onClick={handleProfile}>
                    <FaUser className='icon' />
                    <span>Profile</span>
                </li>

                <li>
                    <IoMdSettings className='icon' />
                    <span>Settings</span>
                </li>

                <li style={{ borderBottomLeftRadius: 10, borderBottomRightRadius: 10 }} onClick={handleLogout}>
                    <FaSignOutAlt className='icon' />
                    <span>Logout</span>
                </li>

            </ul>
        </div>
    )
}

export default DropDownProfile