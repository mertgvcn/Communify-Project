import React from 'react'
import { Routes, Route, Outlet } from 'react-router-dom'
//components
import Navbar from '../components/Navbar/Navbar'
//pages
import SetPasswordPage from '../pages/SetPasswordPage/SetPasswordPage'
import ErrorPage from '../pages/ErrorPage/ErrorPage'

const RouterUnAuthorizedUser = () => {

    const Layout = () => {
        return (
            <>
                <Navbar isLogin={true} />
                <Outlet />
            </>
        )
    }
    
    return (
        <>
            <Routes>
                <Route path='/' element={<Layout />}>
                    <Route path="/" element={<SetPasswordPage />} />
                    <Route path="/setpassword" element={<SetPasswordPage />} />
                    <Route path="*" element={<ErrorPage />} />
                </Route>
            </Routes>
        </>
    )
}

export default RouterUnAuthorizedUser