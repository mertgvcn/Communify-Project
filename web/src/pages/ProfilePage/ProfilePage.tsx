import React, { useEffect, useState } from 'react'
//css
import './ProfilePage.css'
//icons
import { FaUserCircle } from 'react-icons/fa'
import { IoIosInformationCircle } from "react-icons/io";
//models
import { ProfilePageViewModel } from '../../models/pageViewModels/ProfilePageViewModel';
import { UserInformationViewModel } from '../../models/viewModels/UserInformationViewModel';
//helpers
import toast, { Toaster } from 'react-hot-toast';
import { useLocation } from 'react-router-dom';
import { getProfilePageData, getUserInformation, toggleFollowUser } from '../../utils/apis/UserProfileAPI';
import { changePassword } from '../../utils/apis/AuthenticationAPI';
//components
import PrimaryButton from '../../components/Elements/Buttons/PrimaryButton/PrimaryButton';
import EditProfile from './components/EditProfile/EditProfile';
import SecondaryButton from '../../components/Elements/Buttons/SecondaryButton/SecondaryButton';

const ProfilePage = () => {
    const location = useLocation()

    //States
    const [buttonBlocker, setButtonBlocker] = useState(false)

    const [profilePageData, setProfilePageData] = useState<ProfilePageViewModel>({
        userInformationSummary: null,
        profileStats: null,
        profileStatus: null,
        isSuccess: false
    });

    const [editProfileData, setEditProfileData] = useState<UserInformationViewModel | null>(null)
    const [editProfileState, setEditProfileState] = useState(false)

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

        const response = await toggleFollowUser(location.state.username, profilePageData.profileStatus!.isFollower) //TODO: response false dönerse guesttir login popup aç

        await toast(
            `You ${profilePageData.profileStatus!.isFollower ? 'unfollowed' : 'started following'} ${location.state.username}`,
            {
                icon: <IoIosInformationCircle style={{fontSize: 24, color: "#174540"}}/>
            }       
        )

        if (profilePageData.profileStatus!.isFollower)
            profilePageData.profileStats!.followerCount--
        else
            profilePageData.profileStats!.followerCount++


        setProfilePageData({
            ...profilePageData,
            profileStatus: {
                isOwner: false,
                isFollower: !profilePageData.profileStatus!.isFollower
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
        setButtonBlocker(true)

        const response = changePassword()

        await toast.promise(
            response,
            {
                loading: 'Email sending...',
                success: <b>Email successfully sent.</b>,
                error: null
            }
        )

        setTimeout(() => {
            setButtonBlocker(false)
        }, 2000)
    }

    return profilePageData.isSuccess ? (
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
                                <span className='full-name'>{`${profilePageData.userInformationSummary?.firstName} ${profilePageData.userInformationSummary?.lastName}`}</span>
                                <span className='username'>{`#${profilePageData.userInformationSummary?.username}`}</span>
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
                                <span className='stat-value'>{profilePageData.profileStats?.followerCount}</span>
                            </div>
                            <div className='stat'>
                                <span className='stat-title'>Following</span>
                                <span className='stat-value'>{profilePageData.profileStats?.followingCount}</span>
                            </div>
                        </div>

                        <div className="line"></div>

                        {profilePageData.profileStatus!.isOwner ?
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

                                    <PrimaryButton width={70} height={30} value='Reset' fontSize={12} onClickFunction={handleChangePassword} disabled={buttonBlocker} />
                                </div>

                            </div>
                            :
                            <div className='button-wrapper'>
                                <PrimaryButton width={"48%"} height={36} fontSize={14}
                                    value={profilePageData.profileStatus?.isFollower ? 'Unfollow' : 'Follow'}
                                    onClickFunction={handleToggleFollow} disabled={buttonBlocker}
                                />

                                <SecondaryButton width={"48%"} height={36} value='Chat'
                                    fontSize={14} onClickFunction={() => { }} disabled={buttonBlocker} />
                            </div>
                        }

                    </div>
                </div>

            </div>

            {editProfileState && <EditProfile editProfileData={editProfileData} setEditProfileData={setEditProfileData} setEditProfileState={setEditProfileState} />}
        </>

    ) : null
}

export default ProfilePage