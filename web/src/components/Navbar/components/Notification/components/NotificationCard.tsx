import React from 'react'
//css
import './NotificationCard.css'
//icon
import { FaUserCircle } from 'react-icons/fa'

const NotificationCard = () => {

    const handleNotification = () => {
        
    }

    return (
        <div className='notification-card-wrapper' onClick={handleNotification}>
            <div className='icon'>
                <FaUserCircle />
            </div>

            <div className='info'>
                <span className='message'>ocy has started following you</span>
                <span className='date'>09/05/2024 23:51</span>
            </div>
        </div>
    )
}

export default NotificationCard