export const getAllProperties = async () => {
    const response = await fetch('http://localhost:5183/api/Property', {
        method: 'GET',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
    });

    if (response.ok) {
        return await response.json();
    } else {
        throw new Error('Failed to fetch properties');
    }
};

export const addNewProperty = async (propertyData: { propertyName: string }) => {
    const response = await fetch('http://localhost:5183/api/Property', {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(propertyData),
    });
    if (response.ok) {
        // return await response.json();
        return true
    } else {
        throw new Error('Failed to add new property');
    }
};
