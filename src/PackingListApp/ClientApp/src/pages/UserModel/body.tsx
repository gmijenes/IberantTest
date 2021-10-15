import * as React from 'react'
import { message, Menu, Dropdown, Button, Form, Spin, Select, Input, Checkbox, Modal, Row, Col, Alert, InputNumber, Table } from 'antd';
import { FormComponentProps } from 'antd/lib/form';
let FormItem = Form.Item;
import { NewUserItem, NewUserItemStore } from 'src/stores/user-store';
import { connect } from 'redux-scaffolding-ts'
import { nameof } from 'src/utils/object';
import autobind from 'autobind-decorator';
import { GetFieldDecoratorOptions } from 'antd/lib/form/Form';
import { formatMessage } from 'src/services/http-service';
import BooleanInput  from 'src/components/form/booleanInput';
import {
    UserItemsStore,
    UserItem
} from "src/stores/user-store";
import { ItemState } from '../../stores/dataStore';
import { CommandResult } from '../../stores/types';


interface NewUserItemViewProps {
    onClose: (id: string | undefined, item?: NewUserItem) => void;
    itemToEdit: UserItem | undefined,
    onSaveItem?: (item: UserItem, state: ItemState) => Promise<CommandResult<any>>;

}

interface NewUserItemViewState {

}

interface ClassFormBodyProps {
    item: NewUserItem | UserItem | undefined,
    onSave?: () => Promise<any>;
    setFieldsValue(obj: Object): void;
    getFieldValue(fieldName: string): any;
    getFieldDecorator<T extends Object = {}>(id: keyof T, options?: GetFieldDecoratorOptions): (node: React.ReactNode) => React.ReactNode;
}

interface ClassFormBodyState {
    boolvalue: boolean;
    current: string;
}


export class UserItemFormBody extends React.Component<ClassFormBodyProps, ClassFormBodyState> {
    constructor(props: ClassFormBodyProps) {
        super(props);
        this.state = {
            boolvalue: this.props?.item?.isAdmin ?? false,
            current: this.props?.item?.adminType?.toString() ?? '0'
        };
        this.onChangeboolean = this.onChangeboolean.bind(this);


    }

    onChangeboolean(item: boolean) {
        this.setState({ boolvalue: item });
        //this.SetState({ current: "0"})
    }

    onSelectAdmin(item: number) {
        
    }


    render() {

        const { getFieldDecorator } = this.props;
        var item = this.props.item || {} as NewUserItem;
        function handleButtonClick(e) {
            message.info('Click on left button.');
        }

        const handleMenuClick = (e) => {
            var key = e.key
           getFieldDecorator(nameof<NewUserItem>('adminType'), {
                initialValue: key,
            })
            this.setState({ current: key })
            console.log('click left button', key);
        }

        var enumValues = { 0: "Normal", 1: "Vip", 2: "King" }

        const menu = (
            <Menu onClick={handleMenuClick} defaultOpenKeys={['0']}
                selectedKeys={[this.state.current]}>
                <Menu.Item key="0" >
                    Normal
                </Menu.Item>
                <Menu.Item key="1" >
                    Vip
                </Menu.Item>
                <Menu.Item key="2" >
                    King
                </Menu.Item>
            </Menu>
        );


       
        return <Form id="modaForm" onSubmit={() => { if (this.props.onSave) { this.props.onSave(); } }}>
            <Row gutter={24}>

                <Col span={12}>
                    <FormItem label={"Name"}>
                        {getFieldDecorator(nameof<NewUserItem>('name'), {
                            initialValue: item.name,
                        })(
                            <Input />
                        )}
                    </FormItem>
                </Col>
                <Col span={12}>
                    <FormItem label={'Last Name'}>
                        {getFieldDecorator(nameof<NewUserItem>('lastName'), {
                            initialValue: item.lastName,
                        })(
                            <Input />
                        )}
                    </FormItem>
                </Col>
                <Col span={12}>
                    <FormItem label={'Address'}>
                        {getFieldDecorator(nameof<NewUserItem>('address'), {
                            initialValue: item.address,
                        })(
                            <Input />
                        )}
                    </FormItem>
                </Col>

                <Col span={12}>
                    <FormItem label={''}>
                        {getFieldDecorator(nameof<NewUserItem>('isAdmin'), {
                            initialValue: item.isAdmin,
                        })(
                            <BooleanInput value={this.state.boolvalue} onChange={this.onChangeboolean} label={"Is Admin"} />
                        )}
                    </FormItem>
                    {/*<FormItem label={''}>*/}
                    {/*    {getFieldDecorator(nameof<NewUserItem>('adminType'), {*/}
                    {/*        initialValue: item.adminType,*/}
                    {/*    })}*/}
                    {/*</FormItem>*/}
                    <FormItem label={''}>
                        {getFieldDecorator(nameof<NewUserItem>('id'), {
                            initialValue: item.id,
                        })}
                    </FormItem>

                </Col>
                <Col span={12}>
                    {this.state.boolvalue && 
                        <Dropdown.Button onClick={handleButtonClick} overlay={menu} >
                            Admin Type
                        </Dropdown.Button>
                    }
                </Col>
                               
            </Row>
           

        </Form>
    }
}

@connect(["newUserItem", NewUserItemStore])
class NewUserItemView extends React.Component<NewUserItemViewProps & FormComponentProps, NewUserItemViewState> {
    private get UserItemsStore() {
        return (this.props as any).newUserItem as NewUserItemStore;
    }

    constructor(props: NewUserItemViewProps & FormComponentProps) {
        super(props);
        this.UserItemsStore.createNew({} as any);
    }

    componentWillReceiveProps(nextProps: NewUserItemViewProps) {
        if (this.UserItemsStore.state.result && this.UserItemsStore.state.result.isSuccess)
            nextProps.onClose((this.UserItemsStore.state.result as any).aggregateRootId, this.UserItemsStore.state.item)
    }

    @autobind
    private onCreateNewItem() {
        var self = this;
        return new Promise((resolve, reject) => {


            self.props.form.validateFields(event => {
                var values = self.props.form.getFieldsValue();
                if (!event) {
                    values = { ...values, };
                    self.UserItemsStore.change(values);
                    self.UserItemsStore.submit().then(result => {
                        if (result.isSuccess) {
                            resolve();
                        } else {
                            reject();
                        }
                    });
                }
            });
        })
    }

    @autobind
    private onOkButton() {
        if (this.props.itemToEdit) {
            this.onEditItem();
        }
        else
        { this.onCreateNewItem(); }
    }

    @autobind
    private onCancelNewItem() {
        this.UserItemsStore.clear();
        this.props.onClose(undefined);
    }


    @autobind
    private onEditItem() {
        var self = this;
        self.props.form.validateFields(event => {
            var values = self.props.form.getFieldsValue();
            if (!event) {
                values = { ...values, };
                self.UserItemsStore.change(values);
                self.props.onSaveItem(values, 'Unchanged')
                console.log( this.props.itemToEdit)
                this.onCancelNewItem()
            }
        });
    }
        

    

    public render() {
    const { getFieldDecorator } = this.props.form;
    return (
        <Modal
            maskClosable={false}
            visible
            onCancel={this.onCancelNewItem}
            onOk={this.onOkButton}
                closable={false}
                width='800px'
                title={"New UserItem"}>
                {this.UserItemsStore.state.result && !this.UserItemsStore.state.result.isSuccess &&
                    <Alert type='error'
                    message="Ha ocurrido un error"
                    description={formatMessage(this.UserItemsStore.state.result)}
                    />
                }
                <Spin spinning={this.UserItemsStore.state.isBusy}>
                    
                    <UserItemFormBody item={this.props.itemToEdit ?? this.UserItemsStore.state.item} getFieldDecorator={getFieldDecorator} getFieldValue={this.props.form.getFieldValue} setFieldsValue={this.props.form.setFieldsValue} onSave={this.onCreateNewItem} />
                </Spin>
            </Modal>
        );
    }
}

// Wire up the React component to the Redux store
export default Form.create({})(NewUserItemView as any) as any as React.ComponentClass<NewUserItemViewProps>;


