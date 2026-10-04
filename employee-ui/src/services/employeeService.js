// import { getEmployees } from "../services/employeeService";
// import { createEmployee } from "../services/employeeService";
import axios from "axios";

const API_URL = "https://localhost:7184/api/Employees";

export const getEmployees = async () => {
    const response = await axios.get(API_URL);
    return response.data;
};                                      

export const createEmployee = async (employee) => {
    const response = await axios.post(API_URL, employee);
    return response.data;
};

export const getEmployeeById = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
};

export const updateEmployee = async (id, employee) => {
    const response = await axios.put(`${API_URL}/${id}`, employee);
    return response.data;
};

export const deleteEmployee = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
};