import * as React from 'react';
import { useEffect, useState } from 'react';
import { useTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableFooter from '@mui/material/TableFooter';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import IconButton from '@mui/material/IconButton';
import FirstPageIcon from '@mui/icons-material/FirstPage';
import KeyboardArrowLeft from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRight from '@mui/icons-material/KeyboardArrowRight';
import LastPageIcon from '@mui/icons-material/LastPage';
import Modal from '@mui/material/Modal';
import Typography from '@mui/material/Typography';
import PreviewIcon from '@mui/icons-material/Preview';
import api from "../utils/axiosInstance";
import NavBarShape from "../components/NavBarShape.tsx";
import {Button, Container, CssBaseline, Grid, TableHead} from "@mui/material";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import {NavLink} from "react-router-dom";
import DeleteIcon from '@mui/icons-material/Delete';
function getCrawlTypeText(crawlType: number): string {
    switch (crawlType) {
        case 0:
            return "All Types";
        case 1:
            return "On Sale";
        case 2:
            return "Normal Price";
        default:
            return "";
    }
}

function getRequestedAmountAll(requestedAmount: number): string {
    if (requestedAmount === 2147483647) {
        return "All";
    } else {
        return requestedAmount.toString();
    }
}

interface TablePaginationActionsProps {
    count: number;
    page: number;
    rowsPerPage: number;
    onPageChange: (
        event: React.MouseEvent<HTMLButtonElement>,
        newPage: number
    ) => void;
}

function TablePaginationActions(props: TablePaginationActionsProps) {
    const theme = useTheme();
    const { count, page, rowsPerPage, onPageChange } = props;

    const handleFirstPageButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, 0);
    };

    const handleBackButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, page - 1);
    };

    const handleNextButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, page + 1);
    };

    const handleLastPageButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(
            event,
            Math.max(0, Math.ceil(count / rowsPerPage) - 1)
        );
    };

    return (
        <Box sx={{ flexShrink: 0, ml: 2.5 }}>
            <IconButton
                onClick={handleFirstPageButtonClick}
                disabled={page === 0}
                aria-label="first page"
            >
                {theme.direction === 'rtl' ? (
                    <LastPageIcon />
                ) : (
                    <FirstPageIcon />
                )}
            </IconButton>
            <IconButton
                onClick={handleBackButtonClick}
                disabled={page === 0}
                aria-label="previous page"
            >
                {theme.direction === 'rtl' ? (
                    <KeyboardArrowRight />
                ) : (
                    <KeyboardArrowLeft />
                )}
            </IconButton>
            <IconButton
                onClick={handleNextButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="next page"
            >
                {theme.direction === 'rtl' ? (
                    <KeyboardArrowLeft />
                ) : (
                    <KeyboardArrowRight />
                )}
            </IconButton>
            <IconButton
                onClick={handleLastPageButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="last page"
            >
                {theme.direction === 'rtl' ? (
                    <FirstPageIcon />
                ) : (
                    <LastPageIcon />
                )}
            </IconButton>
        </Box>
    );
}

export default function TablePages() {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(5);

    const [productsPage, setProductsPage] = React.useState(0);
    const [productsRowsPerPage, setProductsRowsPerPage] = React.useState(5);

    const [eventsPage, setEventsPage] = React.useState(0);
    const [eventsRowsPerPage, setEventsRowsPerPage] = React.useState(5);


    const [data, setData] = useState([]);
    const [selectedOrder, setSelectedOrder] = useState([]);
    const [selectedEvent, setSelectedEvent] = useState([]);

    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - data.length) : 0;

    const productsEmptyRows =
        productsPage > 0
            ? Math.max(0, (1 + productsPage) * productsRowsPerPage - data.length)
            : 0;

    const eventsEmptyRows =
        eventsPage > 0
            ? Math.max(0, (1 + eventsPage) * eventsRowsPerPage - data.length)
            : 0;

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get('/ProductCrawler/GetAllOrdersAsync');
                setData(response.data);
            } catch (error) {
                console.log(error);
            }
        };

        fetchData();
    }, []);

    const handleChangePage = (
        event: React.MouseEvent<HTMLButtonElement> | null,
        newPage: number
    ) => {
        setPage(newPage);
    };

    const handleChangeProductsPage = (
        event: React.MouseEvent<HTMLButtonElement> | null,
        newProductsPage: number
    ) => {
        setProductsPage(newProductsPage);
    };

    const handleChangeEventsPage = (
        event: React.MouseEvent<HTMLButtonElement> | null,
        newEventsPage: number
    ) => {
        setEventsPage(newEventsPage);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleChangeProductsRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        setProductsRowsPerPage(parseInt(event.target.value, 10));
        setProductsPage(0);
    };

    const handleChangeEventsRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        setEventsRowsPerPage(parseInt(event.target.value, 10));
        setEventsPage(0);
    };

    const handleOpenOrderModal = (order: any) => {
        setSelectedOrder(order.products);
    };

    const handleCloseOrderModal = () => {
        setSelectedOrder([]);
    };

    const handleOpenEventModal = (event: any) => {
        setSelectedEvent(event);
    };

    const handleCloseEventModal = () => {
        setSelectedEvent([]);
    };

    const handleDeleteOrder = async (orderId: string) => {
        try {
            await api.delete(`/ProductCrawler/DeleteOrderAsync/${orderId}`);
            const response = await api.get('/ProductCrawler/GetAllOrdersAsync');
            setData(response.data);
        } catch (error) {
            console.log(error);
            alert("Error");
        }
    };

    return (
        <>
            <NavBarShape />
            <CssBaseline />
            <Container fixed>
                <Box
                    sx={{
                        minWidth: 900,
                        minHeight: 580,
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                        marginTop: 10,
                        zIndex: 1,
                    }}
                >
                    <Card
                        variant="outlined"
                        style={{
                            width: 900,
                            height: 580,
                            backgroundColor: "rgba(255, 255, 255, 0.50)",
                            boxShadow: "0 4px 30px rgba(0, 0, 0, 0.1)",
                            borderRadius: "16px",
                            backdropFilter: "blur(5px)",
                            border: "1px solid rgba(255, 255, 255, 0.3)",
                            position: "relative",
                            zIndex: 1,
                        }}
                    >
                        <CardContent>
                <TableContainer component={Paper}>
                    <Grid container alignItems="center" justifyContent="flex-end">
                        <Grid item>
                            <NavLink to="/profile" style={{ textDecoration: "none" }}>
                                <IconButton size="small" color="primary">
                                    <ArrowBackIcon />
                                </IconButton>
                            </NavLink>
                        </Grid>
                    </Grid>
                    <Typography variant="h4" style={{ color: '#B97A95', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '10px' }} align="left">
                        Recent Orders
                    </Typography>


                    <Table size="small" sx={{ minWidth: 850 }} aria-label="custom pagination table">

                        <TableHead>

                            <TableRow>

                                <TableCell>Order ID</TableCell>
                                <TableCell align="center">Requested Amount</TableCell>
                                <TableCell align="center">Total Found Amount</TableCell>
                                <TableCell align="center">Crawl Type</TableCell>
                                <TableCell align="center">Products</TableCell>
                                <TableCell align="center">Events</TableCell>
                                <TableCell align="center"></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {(rowsPerPage > 0
                                    ? data.slice(
                                        page * rowsPerPage,
                                        page * rowsPerPage + rowsPerPage
                                    )
                                    : data
                            ).map((row: any) => (
                                <TableRow key={row.id}>
                                    <TableCell component="th" scope="row">
                                        {row.id}
                                    </TableCell>
                                    <TableCell style={{ width: 160 }} align="center">
                                        {getRequestedAmountAll(row.requestedAmount)}
                                    </TableCell>
                                    <TableCell style={{ width: 160 }} align="center">
                                        {row.totalFoundAmount}
                                    </TableCell>
                                    <TableCell style={{ width: 160 }} align="center">
                                        {getCrawlTypeText(row.crawlType)}
                                    </TableCell>
                                    <TableCell style={{ width: 70 }} align="center">
                                        <IconButton
                                            size="small"
                                            color="primary"
                                            onClick={() => handleOpenOrderModal(row)}
                                        >
                                            <PreviewIcon />
                                        </IconButton>
                                    </TableCell>
                                    <TableCell style={{ width: 70 }} align="center">
                                        <IconButton
                                            size="small"
                                            color="primary"
                                            onClick={() => handleOpenEventModal(row.orderEvents)}
                                        >
                                            <PreviewIcon />
                                        </IconButton>
                                    </TableCell>
                                    <TableCell style={{ width: 70 }} align="center">
                                        <IconButton
                                            size="small"
                                            color="primary"
                                            onClick={() => handleDeleteOrder(row.id)}
                                        >
                                            <DeleteIcon />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                            {emptyRows > 0 && (
                                <TableRow style={{ height: 53 * emptyRows }}>
                                    <TableCell colSpan={6} />
                                </TableRow>
                            )}
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                <TablePagination
                                    rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
                                    colSpan={6}
                                    count={data.length}
                                    rowsPerPage={rowsPerPage}
                                    page={page}
                                    SelectProps={{
                                        inputProps: { 'aria-label': 'rows per page' },
                                        native: true,
                                    }}
                                    onPageChange={handleChangePage}
                                    onRowsPerPageChange={handleChangeRowsPerPage}
                                    ActionsComponent={TablePaginationActions}
                                />
                            </TableRow>
                        </TableFooter>
                    </Table>
                </TableContainer>
                        </CardContent>

                    </Card>
                </Box>
            </Container>

            <Modal
                open={selectedOrder.length > 0}
                onClose={handleCloseOrderModal}
                aria-labelledby="order-modal-title"
                aria-describedby="order-modal-description"
            >
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        backgroundColor: 'rgba(255, 255, 255, 0.8)',
                        backdropFilter: 'blur(5px)',
                        boxShadow: 24,
                        p: 1,
                    }}
                >

                    <TableContainer component={Paper}>
                        <Typography variant="h4" style={{ color: '#ff9a76', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '10px' }} align="left" id="order-modal-title">
                            Order Details (Products)
                        </Typography>
                        <Table size="small">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Product ID</TableCell>
                                    <TableCell>Name</TableCell>
                                    <TableCell>Picture</TableCell>
                                    <TableCell>Is On Sale</TableCell>
                                    <TableCell>Price</TableCell>
                                    <TableCell>Sale Price</TableCell>
                                    <TableCell>Created On</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {(productsRowsPerPage > 0
                                        ? selectedOrder.slice(
                                            productsPage * productsRowsPerPage,
                                            productsPage * productsRowsPerPage + productsRowsPerPage
                                        )
                                        : selectedOrder
                                ).map((product: any) => (
                                    <TableRow key={product.id}>
                                        <TableCell>{product.id}</TableCell>
                                        <TableCell>{product.name}</TableCell>
                                        <TableCell>{product.picture}</TableCell>
                                        <TableCell>
                                            {product.isOnSale ? 'Yes' : 'No'}
                                        </TableCell>
                                        <TableCell>{product.price}</TableCell>
                                        <TableCell>{product.salePrice}</TableCell>
                                        <TableCell>{product.createdOn}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                            <TableFooter>
                                <TableRow>
                                    <TablePagination
                                        rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
                                        colSpan={7}
                                        count={selectedOrder.length}
                                        rowsPerPage={productsRowsPerPage}
                                        page={productsPage}
                                        SelectProps={{
                                            inputProps: { 'aria-label': 'rows per page' },
                                            native: true,
                                        }}
                                        onPageChange={handleChangeProductsPage}
                                        onRowsPerPageChange={handleChangeProductsRowsPerPage}
                                        ActionsComponent={TablePaginationActions}
                                    />
                                </TableRow>
                            </TableFooter>
                        </Table>
                    </TableContainer>
                </Box>
            </Modal>

            <Modal
                open={selectedEvent.length > 0}
                onClose={handleCloseEventModal}
                aria-labelledby="event-modal-title"
                aria-describedby="event-modal-description"
            >
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        backgroundColor: 'rgba(255, 255, 255, 0.8)',
                        backdropFilter: 'blur(5px)',
                        boxShadow: 24,
                        p: 1,
                    }}
                >

                    <TableContainer component={Paper}>
                        <Typography variant="h4" style={{ color: '#ff9a76', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '10px' }} align="left" id="event-modal-title">
                            Order Details (Events)
                        </Typography>
                        <Table size="small">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Event ID</TableCell>
                                    <TableCell>Status</TableCell>
                                    <TableCell>Created On</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {(eventsRowsPerPage > 0
                                        ? selectedEvent.slice(
                                            eventsPage * eventsRowsPerPage,
                                            eventsPage * eventsRowsPerPage + eventsRowsPerPage
                                        )
                                        : selectedEvent
                                ).map((event: any) => (
                                    <TableRow key={event.id}>
                                        <TableCell>{event.orderId}</TableCell>
                                        <TableCell>{event.status}</TableCell>
                                        <TableCell>{event.createdOn}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                            <TableFooter>
                                <TableRow>
                                    <TablePagination
                                        rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
                                        colSpan={3}
                                        count={selectedEvent.length}
                                        rowsPerPage={eventsRowsPerPage}
                                        page={eventsPage}
                                        SelectProps={{
                                            inputProps: { 'aria-label': 'rows per page' },
                                            native: true,
                                        }}
                                        onPageChange={handleChangeEventsPage}
                                        onRowsPerPageChange={handleChangeEventsRowsPerPage}
                                        ActionsComponent={TablePaginationActions}
                                    />
                                </TableRow>
                            </TableFooter>
                        </Table>
                    </TableContainer>

        </Box>
            </Modal>
        </>
    );
}

