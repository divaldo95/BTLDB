import React, { useState } from 'react';
import { FloatingLabel, Form, Button, Container, Row, Col, Spinner, Alert} from 'react-bootstrap';
import axios from 'axios';
import ArrayDataTable from './ArrayDataTable';

function Home() {
    const [barcode, setBarcode] = useState('');
    const [file, setFile] = useState(null);

    const [data, setData] = useState(null);
    const [dataLoaded, setDataLoaded] = useState(false);
    const [queryErrorMessage, setQueryErrorMessage] = useState("");

    const [isUploading, setIsUploading] = useState(false);
    const [isUploadOK, setIsUploadOK] = useState(false);
    const [uploadMessage, setUploadMessage] = useState("");

    const handleBarcodeChange = (e) => {
        setBarcode(e.target.value);
    };

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const handleBarcodeSubmit = async () => {
        try {
            const response = await axios.get('BTLDB/' + barcode + '/');
            console.log(response.data);
            setData(response.data);
            setDataLoaded(true);
        } catch (error) {
            console.error('There was an error sending the barcode!', error);
            setDataLoaded(false);
            setQueryErrorMessage(error.message);
        }
    };

    const handleFileSubmit = async () => {
        setIsUploading(true);
        const formData = new FormData();
        formData.append('file', file);

        try {
            const response = await axios.post('BTLDB/upload/', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            console.log(response.data);
            setIsUploadOK(true);
        } catch (error) {
            console.error('There was an error uploading the file!', error);
            setUploadMessage(error.message);
            setIsUploadOK(false);
        }
        setIsUploading(false);
    };

    const renderUploadMessage = () => {
        if (isUploadOK) {
            return (
                <Row className="w-100 mb-3">
                    <Col>
                        <Alert variant="success">
                            Upload successful
                        </Alert >
                    </Col>
                </Row>
            );
        }
        else if (uploadMessage === null) {
            return null;
        }
        else if (uploadMessage === "") {
            return null;
        }
        else if (uploadMessage.length === 0) {
            return null;
        }
        return (
            <Row className="w-100 mb-3">
                <Col>
                    <Alert variant="danger">
                        {uploadMessage}
                    </Alert >
                </Col>
            </Row>
        );
    }

    const renderBarcodeData = () => {
        if (dataLoaded) {
            return (
                <Row className="w-100 mb-3">
                    <Col>
                        <ArrayDataTable data={data}></ArrayDataTable>
                    </Col>
                </Row>
            );
        }
        else if (queryErrorMessage === null) {
            return null;
        }
        else if (queryErrorMessage === "") {
            return null;
        }
        else if (queryErrorMessage.length === 0) {
            return null;
        }
        return (
            <Row className="w-100 mb-3">
                <Col>
                    <Alert variant="danger">
                        {queryErrorMessage}
                    </Alert >
                </Col>
            </Row>
        );
    }

    return (
        <Container className="vh-100 d-flex flex-column align-items-center">
            <Row className="w-100 mb-3">
                <Col>
                    <FloatingLabel controlId="floatingInput" label="Barcode" className="mb-3">
                        <Form.Control
                            type="text"
                            placeholder="Enter barcode"
                            value={barcode}
                            onChange={handleBarcodeChange}
                        />
                    </FloatingLabel>
                </Col>
                <Col xs="auto">
                    <Button variant="primary" onClick={handleBarcodeSubmit} className="mb-3">
                        Submit Barcode
                    </Button>
                </Col>
            </Row>

            {renderBarcodeData()}

            <Row className="w-100 mb-3">
                <Col>
                    <Form.Group controlId="formFile" className="mb-3">
                        <Form.Control type="file" onChange={handleFileChange} />
                    </Form.Group>
                </Col>
                <Col xs="auto">
                    <Button variant="primary" disabled={isUploading} onClick={handleFileSubmit}>
                        {isUploading ? <Spinner
                            as="span"
                            animation="grow"
                            size="sm"
                            role="status"
                            aria-hidden="true"
                        /> : ''}
                        Upload File
                    </Button>
                </Col>
            </Row>
            {renderUploadMessage()}
        </Container>
    );
}

export default Home;