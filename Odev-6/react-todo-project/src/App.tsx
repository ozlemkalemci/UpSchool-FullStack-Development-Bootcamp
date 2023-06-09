import { useEffect, useState } from 'react';
import './App.css';
import { ToDoListItemDto } from './types/ToDoListItemDto.ts';
import { Button, Container, Form, Grid, Header, Icon, Input, Table, Segment, Checkbox } from 'semantic-ui-react';

function App() {
    const [task, setTask] = useState<string>('');
    const [savedTask, setSavedTask] = useState<ToDoListItemDto[]>([]);
    const [sortByDate, setSortByDate] = useState(false);
    useEffect(() => {
        handleToDoItem();
    }, []);

    const handleToDoItem = (): void => {
        setTask('');
    };

    const handleSaveToDoText = () => {
        const sameToDoText = savedTask.find((item) => item.Task === task);
        if (!sameToDoText) {
            const newItem: ToDoListItemDto = new ToDoListItemDto();
            newItem.Task = task;
            setSavedTask([...savedTask, newItem]);
            setTask('')
        }
    };

    const handleSavedToDoTextDelete = (selectedTask: string) => {
        const newSavedTask = savedTask.filter((item) => item.Task !== selectedTask);
        setSavedTask(newSavedTask);
    };

    const handleToggleCompleted = (id: string) => {
        const updatedTaskList = savedTask.map((item) => {
            if (item.Id === id) {
                return {
                    ...item,
                    IsCompleted: !item.IsCompleted,
                };
            }
            return item;
        });
        setSavedTask(updatedTaskList);
    };

    const handleSortByDate = () => {
        setSortByDate(!sortByDate);
    };

    const sortedTasks = savedTask.slice().sort((a, b) => {
        if (sortByDate) {
            return b.CreatedOn.getTime() - a.CreatedOn.getTime();
        } else {
            return a.CreatedOn.getTime() - b.CreatedOn.getTime();
        }
    });

    return (
        <>
            <Container className="App">
                <div>
                    <Header as="h1" icon textAlign="center" color="grey">
                        <Icon name="calendar check outline" circular />
                        <Header.Content>To Do List</Header.Content>
                    </Header>
                </div>
                <Segment
                    raised
                    style={{
                        backgroundColor: 'rgba(252, 252, 243, 0.8)',
                        boxShadow: '0 8px 16px 0 rgba(0,0,0,0.2)',
                        transition: '0.3s',
                        color: '#173A3A',
                    }}
                >
                    <Grid>
                        <Grid.Row columns={1}>
                            <Grid.Column width={16}>
                                <Form>
                                    <Form.Field>
                                        <Input
                                            action={{
                                                id: 'todoitemholder',
                                                color: 'teal',
                                                labelPosition: 'right',
                                                icon: 'add',
                                                content: 'Add',
                                                onClick: handleSaveToDoText,
                                            }}
                                            onChange={(e) => setTask(e.target.value)}
                                            placeholder="Enter a task"
                                            value={task}
                                        />
                                    </Form.Field>
                                </Form>
                            </Grid.Column>
                        </Grid.Row>
                    </Grid>

                    <Header as="h4" style={{ paddingBottom: '15px' }}>
                        TODOS:
                    </Header>

                    <div>
                        <Table color={'olive'}>
                            <Table.Header>
                                <Table.Row>
                                    <Table.HeaderCell style={{ width: '15%' }}>Id</Table.HeaderCell>
                                    <Table.HeaderCell style={{ width: '55%' }}>Task</Table.HeaderCell>
                                    <Table.HeaderCell style={{ width: '10%' }}>Date</Table.HeaderCell>
                                    <Table.HeaderCell style={{ width: '5%' }}>Status</Table.HeaderCell>
                                    <Table.HeaderCell style={{ width: '15%' }} textAlign="right"><Button icon onClick={handleSortByDate}>
                                        Sort By Date{sortByDate ? <Icon name="sort numeric descending"/> : <Icon name="sort numeric ascending"/> }
                                    </Button> </Table.HeaderCell>
                                </Table.Row>
                            </Table.Header>

                            <Table.Body>
                                {sortedTasks.map((item) => (
                                    <Table.Row key={item.Id}>
                                        <Table.Cell style={{ width: '15%' }}>{item.Id}</Table.Cell>
                                        <Table.Cell style={{ width: '55%', textDecoration: item.IsCompleted ? 'line-through' : 'none' }}>{item.Task}</Table.Cell>
                                        <Table.Cell style={{ width: '10%' }}>{item.CreatedOn.toLocaleString()}</Table.Cell>
                                        <Table.Cell style={{ width: '5%' }} textAlign="center">
                                            <Checkbox checked={item.IsCompleted} onChange={() => handleToggleCompleted(item.Id)} />
                                        </Table.Cell>
                                        <Table.Cell textAlign="right" style={{ width: '15%' }}>
                                            <Button icon onClick={() => handleSavedToDoTextDelete(item.Task)}>
                                                <Icon name="trash" />
                                            </Button>
                                        </Table.Cell>
                                    </Table.Row>
                                ))}
                            </Table.Body>
                        </Table>
                    </div>
                </Segment>
            </Container>
        </>
    );
}

export default App;
