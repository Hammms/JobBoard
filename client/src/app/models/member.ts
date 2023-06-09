import { Photo } from "./photo";

    export interface Member {
        id: number;
        username: string;
        photoUrl: string;
        age: number;
        emailAddress?: any;
        created: Date;
        lastActive: Date;
        dateOfBirth: Date;
        knownAs: string;
        gender: string;
        introduction: string;
        lookingFor: string;
        interests: string;
        city: string;
        country: string;
        resume: any[];
        photos: Photo[];
    }


