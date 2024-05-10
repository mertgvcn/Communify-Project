import { UserInformationSummaryViewModel } from "../viewModels/UserInformationSummaryViewModel"

export type ProfilePageDataModel = {
    userInformationSummary: UserInformationSummaryViewModel | null,
    profileStatus : ProfileStatusType
}

type ProfileStatusType = {
    isOwner: boolean,
    isFollower: boolean
}