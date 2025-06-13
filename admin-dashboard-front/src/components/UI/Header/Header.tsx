import classes from './Header.module.scss';

const Header = () => {
    return (
        <header className={classes.header}>
            <div className={classes.title}>
                Admin Dashboard
            </div>
        </header>
    );
};

export default Header;