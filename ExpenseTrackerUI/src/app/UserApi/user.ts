export interface User {
    id: string;
    email: string;
  }
  
export interface UserAuth {
  email: string;
  password: string;
}

export const url = "http://localhost:49478";