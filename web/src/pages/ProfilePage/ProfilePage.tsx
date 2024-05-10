import React, { useEffect, useState } from 'react'
//css
import './ProfilePage.css'
//icons
import { FaUserCircle } from 'react-icons/fa'
//models
import { UserInformationSummaryViewModel } from '../../models/viewModels/UserInformationSummaryViewModel';
import { UserInformationViewModel } from '../../models/viewModels/UserInformationViewModel';
//helpers
import toast, { Toaster } from 'react-hot-toast';
import { getUserInformationSummary } from '../../utils/apis/NavbarAPI';
import { getUserInformation } from '../../utils/apis/UserProfileAPI';
import { changePassword } from '../../utils/apis/AuthenticationAPI';
//components
import PrimaryButton from '../../components/Elements/Buttons/PrimaryButton/PrimaryButton';
import EditProfile from './components/EditProfile/EditProfile';
import ChangePassword from './components/ChangePassword/ChangePassword';

const ProfilePage = () => {

    const [userInformationSummary, setUserInformationSummary] = useState<UserInformationSummaryViewModel | null>(null)
    const [isOwner, setIsOwner] = useState(true); //ToDo: burayı prop olarak alıcaz. Güvenliğe dikkat etmek lazım.
    const [buttonBlocker, setButtonBlocker] = useState(false)

    const [editProfileData, setEditProfileData] = useState<UserInformationViewModel | null>(null)
    const [editProfileState, setEditProfileState] = useState(false)

    const [changePasswordState, setChangePasswordState] = useState(false)

    useEffect(() => {
        fetchUserInformationSummary()
    }, [])

    const fetchUserInformationSummary = async () => {
        const response = await getUserInformationSummary()
        setUserInformationSummary(response)
    }

    const handleEditProfile = async () => {
        const response = await getUserInformation()

        setEditProfileData(response)
        setEditProfileState(true)
    }

    const handleChangePassword = async () => {
        setChangePasswordState(true)
    }

    return userInformationSummary ? (
        <>
            <div className='profile-page-wrapper'>
                <Toaster toastOptions={{ style: { fontSize: 14 } }} />

                <div className='post-wrapper'>
                    Posts
                </div>

                <div className='user-details-wrapper'>
                    <div className="user-details-container">

                        <div className='user-summary-info'>
                            <div className='icon'>
                                <FaUserCircle />
                            </div>

                            <div className='info'>
                                <span className='full-name'>{`${userInformationSummary.firstName} ${userInformationSummary.lastName}`}</span>
                                <span className='username'>{`#${userInformationSummary.username}`}</span>
                            </div>
                        </div>

                        <div className='line'></div>

                        <div className='user-stats'>
                            <div className='stat'>
                                <span className='stat-title'>Posts</span>
                                <span className='stat-value'>24</span>
                            </div>
                            <div className='stat'>
                                <span className='stat-title'>Communities</span>
                                <span className='stat-value'>8</span>
                            </div>
                            <div className='stat'>
                                <span className='stat-title'>Friends</span>
                                <span className='stat-value'>78</span>
                            </div>
                        </div>

                        <div className="line"></div>

                        {isOwner ?
                            <div className='profile-management'>

                                <div className='manager'>
                                    <div className='info'>
                                        <span className='title'>Profile</span>
                                        <span className='description'>Customize your profile</span>
                                    </div>

                                    <PrimaryButton width={70} height={30} value='Edit' fontSize={12} onClickFunction={handleEditProfile} />
                                </div>

                                <div className='manager'>
                                    <div className='info'>
                                        <span className='title'>My Friends</span>
                                        <span className='description'>Manage your friend list</span>
                                    </div>

                                    <PrimaryButton width={70} height={30} value='Manage' fontSize={12} onClickFunction={() => { }} />
                                </div>

                                <div className='manager'>
                                    <div className='info'>
                                        <span className='title'>Password</span>
                                        <span className='description'>Change your password</span>
                                    </div>

                                    <PrimaryButton width={70} height={30} value='Reset' fontSize={12} onClickFunction={handleChangePassword} />
                                </div>

                            </div>
                            :
                            null
                        }

                    </div>
                </div>

            </div>

            {editProfileState && <EditProfile editProfileData={editProfileData} setEditProfileDate={setEditProfileData} setEditProfileState={setEditProfileState} />}
            {changePasswordState && <ChangePassword setChangePasswordState={setChangePasswordState} />}
        </>

    ) : null
}

export default ProfilePage