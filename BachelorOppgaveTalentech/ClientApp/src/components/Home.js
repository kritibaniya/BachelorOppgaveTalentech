import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { appInformation: [], loading: true };
    }

    componentDidMount() {
        this.getApplicationInformation();
    }
    
    static renderAppInformationTable(appInformation) {
        return (
            <div>
                <h2>List of names:</h2>
                <ul>
                    {appInformation.map((file, index) => (
                        <li key={index}>{file.Name}</li>
                    ))}
                </ul>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderAppInformationTable(this.state.appInformation);

        return (
            <div>
                <h1 id="tableLabel">App names</h1>
                 
        {contents}
            </div>
        );
    }

    async getApplicationInformation() {
        const response = await fetch('marketplaceadmin');
        const data = await response.json();
        console.log(data);
        this.setState({ appInformation: data, loading: false });
    }
}
