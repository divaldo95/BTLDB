import { Table } from 'react-bootstrap';

const ArrayDataTable = ({ data }) => {
    console.log(data);

    return (
        <div>
            <h2>Channel data ({data.SN})</h2>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Channel No</th>
                        <th>Vop</th>
                        <th>Id1</th>
                        <th>Id2</th>
                        <th>Is</th>
                    </tr>
                </thead>
                <tbody>
                    {data.Channels.map(channel => (
                        <tr key={channel.ChNo}>
                            <td>{channel.ChNo}</td>
                            <td>{channel.Vop}</td>
                            <td>{channel.Id1}</td>
                            <td>{channel.Id2}</td>
                            <td>{channel.Is}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <h2>Array properties ({data.SN})</h2>
            <Table striped bordered hover>
                <tbody>
                    <tr>
                        <td>Mean Resistance</td>
                        <td>{data.MeanResistance}</td>
                    </tr>
                    <tr>
                        <td>Position No</td>
                        <td>{data.PositionNo}</td>
                    </tr>
                    <tr>
                        <td>RTD</td>
                        <td>{data.RTD}</td>
                    </tr>
                    <tr>
                        <td>SN</td>
                        <td>{data.SN}</td>
                    </tr>
                    <tr>
                        <td>Standard Deviation Resistance</td>
                        <td>{data.StdDevResistance}</td>
                    </tr>
                    <tr>
                        <td>TEC Resistance</td>
                        <td>{data.TECResistance}</td>
                    </tr>
                    <tr>
                        <td>Tray No</td>
                        <td>{data.TrayNo}</td>
                    </tr>
                </tbody>
            </Table>
        </div>
    );
};

export default ArrayDataTable;

