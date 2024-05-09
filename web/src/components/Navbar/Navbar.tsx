import React, { useState } from 'react'
import { Link, } from 'react-router-dom'
//css
import './Navbar.css'
//types
import { FormStateType } from '../Forms/RegisterForm/types/FormStateType'
//models
import { Roles } from '../../enums/Roles';
import { InterestViewModel } from '../../models/viewModels/InterestViewModel'
//icons
import { FaUserCircle } from "react-icons/fa";
import { IoIosNotifications } from "react-icons/io";
//helpers
import { GetInterests } from '../../utils/apis/InterestAPI'
//components
import PrimaryButton from '../Elements/Buttons/PrimaryButton/PrimaryButton'
import SecondaryButton from '../Elements/Buttons/SecondaryButton/SecondaryButton'
import LoginForm from '../Forms/LoginForm/LoginForm'
import RegisterForm from '../Forms/RegisterForm/RegisterForm'
import ForgotPassword from '../Forms/ForgotPasswordForm/ForgotPassword';
import Searchbar from './components/Searchbar/Searchbar';
import UserMenu from './components/UserMenu/UserMenu';

type NavbarType = {
    role: Roles,
    loginFormState?: boolean
}

const Navbar = (props: NavbarType) => {
    const [formState, setFormState] = useState<FormStateType>({
        loginFormState: props.loginFormState ? props.loginFormState : false,
        registerFormState: false
    })
    const [forgotPasswordState, setForgotPasswordState] = useState<boolean>(false)
    const [notificationState, setNotificationState] = useState<boolean>(false)
    const [userMenuState, setUserMenuState] = useState<boolean>(false)

    const [interestList, setInterestList] = useState<InterestViewModel[]>([])

    //functions
    const fetchInterestList = async () => {
        const response: any = await GetInterests()

        setInterestList(response)
    }

    const handleLoginForm = () => {
        setFormState({
            loginFormState: true,
            registerFormState: false
        })
    }

    const handleRegisterForm = async () => {
        await fetchInterestList()

        setFormState({
            loginFormState: false,
            registerFormState: true
        })
    }

    return (
        <>
            <nav className="navbar-wrapper">
                <div className="navbar-components">

                    <div className='column'>
                        <div className="navbar-logo">
                            <Link to='/' className='link'>
                                <img src={require(`../../assets/logos/large_logo.png`)} alt="img not found" />
                            </Link>
                        </div>
                    </div>


                    <div className='column'>
                        <div className='search-bar'>
                            <Searchbar />
                        </div>
                    </div>


                    <div className='column'>
                        <div className='role-based-components'>

                            {props.role == Roles.Guest.valueOf() &&
                                <div className='navbar-buttons'>
                                    <PrimaryButton value='Login' fontSize={16} width="120px" height="40px" onClickFunction={handleLoginForm} />
                                    <SecondaryButton value='Sign Up' fontSize={16} width="120px" height="40px" onClickFunction={handleRegisterForm} />
                                </div>
                            }

                            {props.role == Roles.User.valueOf() &&
                                <>
                                    <div className="button" style={{ fontSize: 36, marginRight: 16 }} onClick={() => { setNotificationState((prev) => !prev) }}>
                                        <IoIosNotifications />
                                    </div>
                                    <div className="button" style={{ fontSize: 36 }} onClick={() => { setUserMenuState((prev) => !prev) }}>
                                        <FaUserCircle />
                                    </div>
                                </>
                            }

                        </div>
                    </div>

                </div>
            </nav >

            {formState.loginFormState && <LoginForm setFormState={setFormState} setForgotPasswordState={setForgotPasswordState} setInterestList={setInterestList} />}
            {formState.registerFormState && <RegisterForm setFormState={setFormState} interestList={interestList} />}
            {forgotPasswordState && <ForgotPassword setFormState={setFormState} setForgotPasswordState={setForgotPasswordState} />}


            {userMenuState && <UserMenu setUserMenuState={setUserMenuState} />}
        </>
    )
}

export default Navbar