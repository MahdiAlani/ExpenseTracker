export interface User {
    id: number,
    email: string
}

export interface UserAuth {
    email: string,
    password: string
}

export const url = "https://localhost:7101"