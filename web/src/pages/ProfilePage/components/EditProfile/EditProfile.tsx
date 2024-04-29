import React from 'react'
//css
import './EditProfile.css'
//icons
import { IoCloseCircleOutline, IoLocationOutline } from 'react-icons/io5'
import { FaUserCircle } from 'react-icons/fa'
import { PiIdentificationCardBold, PiUserBold } from 'react-icons/pi'
import { GrPhone } from 'react-icons/gr'
import { MdOutlineMail } from 'react-icons/md'
import { TbBuildingCommunity } from 'react-icons/tb'
import { FaTrash } from "react-icons/fa";
//models
import { UserInformationViewModel } from '../../../../models/viewModels/UserInformationViewModel'
import { Genders } from '../../../../models/enums/Genders'
//compoenents
import TextInput from '../../../../components/Elements/TextInput/TextInput'
import { format } from 'date-fns'

type EditProfileType = {
    editProfileData: UserInformationViewModel | null,
    setEditProfileDate: React.Dispatch<React.SetStateAction<UserInformationViewModel | null>>,

    setEditProfileState: React.Dispatch<React.SetStateAction<boolean>>
}

const EditProfile = ({ editProfileData, setEditProfileDate, setEditProfileState }: EditProfileType) => {

    return editProfileData ? (
        <div className='edit-profile-background'>
            <div className="edit-profile-wrapper">

                <div className="close-button-wrapper">
                    <IoCloseCircleOutline style={{ float: 'right', cursor: 'pointer' }} onClick={() => setEditProfileState(false)} />
                </div>

                <div className="row">

                    <div className='col' style={{ width: '25%' }}>
                        <div className='user-summary-info'>
                            <div className='icon'>
                                <FaUserCircle />
                            </div>

                            <div className='info'>
                                <span className='full-name'>{editProfileData.firstName}</span>
                                <span className='username'>#{editProfileData.username}</span>
                            </div>
                        </div>

                        <div className="line"></div>

                        <div className="user-immutable-details">
                            <div className="detail">
                                <span className='title'>Birth Date:</span>
                                <span className="value">{format(editProfileData.birthDate, "dd/MM/yyyy")}</span>
                            </div>
                            <div className="detail">
                                <span className='title'>Birth Place:</span>
                                <span className="value">{editProfileData.birthCountry}/{editProfileData.birthCity}</span>
                            </div>
                            <div className="detail">
                                <span className='title'>Gender:</span>
                                <span className="value">{Genders[editProfileData.gender]}</span>
                            </div>
                        </div>
                    </div>

                    <div className="col" style={{ width: '50%' }}>
                        <div className="user-mutable-details">

                            <div className="row" style={{ justifyContent: 'space-between', marginBottom: 8 }}>
                                <div className="detail">
                                    <span className='label'>First Name</span>
                                    <TextInput width={270} height={36} fontSize={16} isPassword={false}
                                        icon={PiUserBold} value={editProfileData.firstName} />
                                </div>

                                <div className="detail">
                                    <span className='label'>Last Name</span>
                                    <TextInput width={270} height={36} fontSize={16} isPassword={false}
                                        icon={PiUserBold} value={editProfileData.lastName} />
                                </div>
                            </div>

                            <div className="row" style={{ justifyContent: 'space-between', marginBottom: 16 }}>
                                <div className="detail">
                                    <span className='label'>Username</span>
                                    <TextInput width={270} height={36} fontSize={16} isPassword={false}
                                        icon={PiIdentificationCardBold} value={editProfileData.username} />
                                </div>

                                <div className="detail">
                                    <span className='label'>Email</span>
                                    <TextInput width={270} height={36} fontSize={16} isPassword={false}
                                        icon={MdOutlineMail} value={editProfileData.email} />
                                </div>
                            </div>

                            <div className="row" style={{ justifyContent: 'space-between', marginBottom: 16 }}>
                                <div className="detail">
                                    <span className='label'>Phone Number</span>
                                    <TextInput width={270} height={36} fontSize={16} isPassword={false}
                                        icon={GrPhone} value={editProfileData.phoneNumber} />
                                </div>

                                <div className="detail">
                                    <span className='label'>Current Place</span>
                                    <TextInput width={270} height={36} fontSize={16} isPassword={false}
                                        icon={IoLocationOutline} value={`${editProfileData.currentCountry}/${editProfileData.currentCity}`} />
                                </div>
                            </div>

                            <div className="detail">
                                <span className='label'>Address</span>
                                <TextInput width={'100%'} height={36} fontSize={16} isPassword={false}
                                    icon={TbBuildingCommunity} value={editProfileData.address} />
                            </div>

                        </div>
                    </div>

                    <div className='col' style={{ width: '25%' }}>
                        <div className="interests-wrapper">
                            <span className='title'>Interests</span>

                            <div className="line" style={{ marginTop: "0.5rem", marginBottom: "1rem" }}></div>

                            {editProfileData.interests.map((interest, idx) => (
                                <div className='interest' key={idx}>
                                    <span className='interest-name'>{interest.name}</span>
                                    <div className='delete-button'>
                                        <FaTrash />
                                    </div>
                                </div>
                            ))}

                        </div>
                    </div>

                </div>

            </div>
        </div>
    ) : null
}

export default EditProfile