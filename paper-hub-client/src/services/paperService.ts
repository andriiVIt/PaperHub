export const getPaperList = async () => {
    const response = await fetch('http://localhost:5183/api/Paper?limit=10&startAt=0', {
        method: "GET", // *GET, POST, PUT, DELETE, etc.
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
    });
    return await response.json();
}