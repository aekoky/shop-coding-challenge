import { PointModel } from './point.model';

export interface UserModel {
    id: string;
    email: string;
    password: string;
    token: string;
    coordinates: PointModel;
}
