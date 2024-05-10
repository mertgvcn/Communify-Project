import React, { useEffect, useState } from 'react'
//css
import './ProfilePage.css'
//icons
import { FaUserCircle } from 'react-icons/fa'
//models
import { ProfilePageDataModel } from '../../models/pageViewModels/ProfilePageDataModel';
import { UserInformationViewModel } from '../../models/viewModels/UserInformationViewModel';
//helpers
import toast, { Toaster } from 'react-hot-toast';
import { useLocation } from 'react-router-dom';
import { getProfilePageData, getUserInformation, toggleFollowUser } from '../../utils/apis/UserProfileAPI';
import { changePassword } from '../../utils/apis/AuthenticationAPI';
//components
import PrimaryButton from '../../components/Elements/Buttons/PrimaryButton/PrimaryButton';
import EditProfile from './components/EditProfile/EditProfile';
import ChangePassword from './components/ChangePassword/ChangePassword';
import SecondaryButton from '../../components/Elements/Buttons/SecondaryButton/SecondaryButton';


const ProfilePage = () => {
    const location = useLocation()

    //States
    const [buttonBlocker, setButtonBlocker] = useState(false)

    const [profilePageData, setProfilePageData] = useState<ProfilePageDataModel>({
        profileStatus: {
            isOwner: false,
            isFollower: false
        },
        userInformationSummary: null
    });

    const [editProfileData, setEditProfileData] = useState<UserInformationViewModel | null>(null)
    const [editProfileState, setEditProfileState] = useState(false)
    
    const [changePasswordState, setChangePasswordState] = useState(false)

    //On page load functions
    useEffect(() => {
        fetchProfilePageData();
    }, [location.state.username])

    const fetchProfilePageData = async () => {
        const response = await getProfilePageData(location.state.username)
        setProfilePageData(response)
    }

    //Profile visitor functions
    const handleToggleFollow = async () => {
        setButtonBlocker(true)

        const response = toggleFollowUser(location.state.username, profilePageData.profileStatus.isFollower)

        await toast.promise(
            response,
            {
                loading: 'Please wait...',
                success: <b>{`You ${profilePageData.profileStatus.isFollower ? 'unfollowed' : 'started following'} ${location.state.username}`}</b>,
                error: null
            }
        )

        setProfilePageData({
            ...profilePageData,
            profileStatus: {
                isOwner: false,
                isFollower: !profilePageData.profileStatus.isFollower
            }
        })

        setButtonBlocker(false)
    }

    //Profile owner functions
    const handleEditProfile = async () => {
        const response = await getUserInformation()

        setEditProfileData(response)
        setEditProfileState(true)
    }

    const handleChangePassword = async () => {
        setChangePasswordState(true)
    }

    return profilePageData.userInformationSummary ? (
        <>
            <div className='profile-page-wrapper'>
                <Toaster toastOptions={{ style: { fontSize: 14 } }} />

                <div className='post-wrapper'>
                    Posts/Community Memberships
                </div>

                <div className='user-details-wrapper'>
                    <div className="user-details-container">

                        <div className='user-summary-info'>
                            <div className='icon'>
                                <FaUserCircle />
                            </div>

                            <div className='info'>
                                <span className='full-name'>{`${profilePageData.userInformationSummary.firstName} ${profilePageData.userInformationSummary.lastName}`}</span>
                                <span className='username'>{`#${profilePageData.userInformationSummary.username}`}</span>
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
                                <span className='stat-title'>Followers</span>
                                <span className='stat-value'>78</span>
                            </div>
                            <div className='stat'>
                                <span className='stat-title'>Following</span>
                                <span className='stat-value'>15</span>
                            </div>
                        </div>

                        <div className="line"></div>

                        {profilePageData.profileStatus.isOwner ?
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
                            <div className='button-wrapper'>
                                <PrimaryButton width={"48%"} height={36} fontSize={14} 
                                    value={profilePageData.profileStatus.isFollower ? 'Unfollow' : 'Follow'}
                                    onClickFunction={handleToggleFollow} disabled={buttonBlocker}
                                />

                                <SecondaryButton width={"48%"} height={36} value='Chat'
                                    fontSize={14} onClickFunction={() => { }} disabled={buttonBlocker} />
                            </div>
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