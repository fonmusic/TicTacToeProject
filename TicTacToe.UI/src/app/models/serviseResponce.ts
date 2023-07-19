export class ServiceResponse<T> {
    data!: T;
    errorMessage!: string;
    success!: boolean;
}